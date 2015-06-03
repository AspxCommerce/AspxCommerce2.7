/*
AspxCommerce® - http://www.aspxcommerce.com
Copyright (c) 2011-2015 by AspxCommerce

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OF OTHER DEALINGS IN THE SOFTWARE. 
*/



using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;

public partial class FileUploader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool isValidFile = false;
        string validExtension=string.Empty;
        string retFilePath = string.Empty;
        string retMsg = "fail";
        int maxFileSize = 0;
        int retStatus = -1;
        string strBaseLocation = string.Empty;
        string filename = string.Empty;
        Random rnd = new Random();
        if (Request.Form["ValidExtension"] != null)
        {
            validExtension = Request.Form["ValidExtension"].ToString();
        }
        if (Request.Form["BaseLocation"] != null)
        {
            strBaseLocation = Request.Form["BaseLocation"].ToString();
        }
        if (Request.Form["MaxFileSize"] != null && Request.Form["MaxFileSize"] !="" && int.Parse(Request.Form["MaxFileSize"].ToString())>0)
        {
            maxFileSize = int.Parse(Request.Form["MaxFileSize"].ToString());
        }
        if (Request.Files != null)
        {
            HttpFileCollection files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFile file = files[i];
                if (file.ContentLength > 0 && file.ContentLength < maxFileSize * 1024)
                {
                    if (validExtension.Length > 0)
                    {
                        string[] allowExtensions = validExtension.Split(' ');
                        if (allowExtensions.Contains(GetExtension(file.FileName), StringComparer.InvariantCultureIgnoreCase))
                        {
                            isValidFile = true;
                            retMsg = "Valid file Extension";
                        }
                        else
                        {
                            retMsg = "Not valid file Extension";
                        }
                    }
                    else
                    {
                        isValidFile = true;
                    }
                    if (isValidFile)
                    {
                        retFilePath = strBaseLocation;
                        strBaseLocation = Server.MapPath("~/" + strBaseLocation);
                        if (!Directory.Exists(strBaseLocation))
                        {
                            Directory.CreateDirectory(strBaseLocation);
                        }
                        filename = System.IO.Path.GetFileName(file.FileName);
                        string strExtension = GetExtension(filename);
                        filename = filename.Substring(0, (filename.Length - strExtension.Length) - 1);
                        filename = filename + '_' + rnd.Next(111111, 999999).ToString() + '.' + strExtension;
                        string filePath = strBaseLocation + "\\" + filename;
                        retFilePath = retFilePath + "/" + filename;
                        file.SaveAs(filePath);
                        retMsg = "File upload successfully";
                        retStatus = 1;
                    }
                }
                else
                {
                    retMsg = "Sorry, the file must be less than " + maxFileSize + "KB";
                }
            }
        }
        Literal lit =new Literal();
        lit.Text = "<pre id='response'>({  'Status': '" + retStatus + "','Message': '" + retMsg + "','UploadedPath': '" + retFilePath + "' })</pre>";
        this.Page.Form.Controls.Add(lit);
    }
    private string GetExtension(string fileName)
    {
        int index = fileName.LastIndexOf('.') ;
        string ext = fileName.Substring(index + 1, (fileName.Length - index)-1);
        return ext;
    }
}
