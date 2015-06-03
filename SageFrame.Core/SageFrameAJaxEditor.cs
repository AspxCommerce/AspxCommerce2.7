/*
SageFrame® - http://www.sageframe.com
Copyright (c) 2009-2010 by SageFrame
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
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AjaxControlToolkit.HTMLEditor;



using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Collections;
using System.Collections.ObjectModel;

using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using System.Globalization;
using System.CodeDom;
using System.Drawing;
using System.IO;
using AjaxControlToolkit;
using AjaxControlToolkit.HTMLEditor.ToolbarButton;

[assembly: WebResource("App_Scripts.HTMLEditor.scripts.InsertDate.js", "application/x-javascript")]
namespace AjaxControlToolkit.HTMLEditor.CustomToolbarButton
{
    /// <summary>
    /// Class that inserts icon image.
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [RequiredScript(typeof(OkCancelPopupButton))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public class InsertIcon : DesignModePopupImageButton
    {

        #region [ Properties ]

        /// <summary>
        /// Gets or sets icon in row.
        /// </summary>
        [DefaultValue(10)]
        [Category("Appearance")]
        [Description("Icons in one row of the ralated popup")]
        public int IconsInRow
        {
            get { return (int)(ViewState["IconsInRow"] ?? 10); }
            set { ViewState["IconsInRow"] = value; }
        }

        /// <summary>
        /// Gets or sets Icons Folder.
        /// </summary>
        [DefaultValue("~/App_Images/HTMLEditor.icons/")]
        [Category("Appearance")]
        [Description("Folder used for icons")]
        public string IconsFolder
        {
            get { return (string)(ViewState["IconsFolder"] ?? "~/App_Images/HTMLEditor.icons/"); }
            set { ViewState["IconsFolder"] = value; }
        }

        #endregion

        #region [ Methods ]

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            RelatedPopup = new CustomPopups.InsertIconPopup();
            (RelatedPopup as CustomPopups.InsertIconPopup).IconsInRow = IconsInRow;
            (RelatedPopup as CustomPopups.InsertIconPopup).IconsFolder = IconsFolder;
        }

        protected override void OnPreRender(EventArgs e)
        {
            RegisterButtonImages("ed_insertIcon");
            base.OnPreRender(e);
        }

        protected override string ClientControlType
        {
            get { return "AjaxControlToolkit.HTMLEditor.CustomToolbarButton.InsertIcon"; }
        }

        /// <summary>
        /// Gets  script path.
        /// </summary>
        public override string ScriptPath
        {
            get { return "~/App_Scripts/HTMLEditor.scripts/InsertIcon.js"; }
        }

        /// <summary>
        /// Gets tooltip.
        /// </summary>
        public override string ToolTip
        {
            get { return "Insert predefined icon"; }
        }

        #endregion
    }

    /// <summary>
    /// Class that inset date.
    /// </summary>
    [ParseChildren(true)]
    [PersistChildren(false)]
    [RequiredScript(typeof(MethodButton))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    public class InsertDate : MethodButton
    {
        #region [ Methods ]

        protected override void OnPreRender(EventArgs e)
        {
            RegisterButtonImages("ed_date");
            base.OnPreRender(e);
        }

        protected override string ClientControlType
        {
            get { return "AjaxControlToolkit.HTMLEditor.CustomToolbarButton.InsertDate"; }
        }

        /// <summary>
        /// Gets the script path.
        /// </summary>
        public override string ScriptPath
        {
            get { return "~/App_Scripts/HTMLEditor.scripts/InsertDate.js"; }
        }

        /// <summary>
        /// Gets the tooltip.
        /// </summary>
        public override string ToolTip
        {
            get { return "Insert current date"; }
        }

        #endregion
    }
}

namespace AjaxControlToolkit.HTMLEditor.CustomPopups
{
    [ParseChildren(true)]
    [RequiredScript(typeof(Popups.AttachedTemplatePopup))]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1501:AvoidExcessiveInheritance")]
    internal class InsertIconPopup : Popups.AttachedTemplatePopup
    {

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the icon in row.
        /// </summary>
        [DefaultValue(10)]
        [Category("Appearance")]
        [Description("Icons in one row")]
        public int IconsInRow
        {
            get { return (int)(ViewState["IconsInRow"] ?? 10); }
            set { ViewState["IconsInRow"] = value; }
        }

        /// <summary>
        /// Gets or sets icon folder name.
        /// </summary>
        [DefaultValue("~/App_Images/HTMLEditor.icons/")]
        [Category("Appearance")]
        [Description("Folder used for icons")]
        public string IconsFolder
        {
            get { return (string)(ViewState["IconsFolder"] ?? "~/App_Images/HTMLEditor.icons/"); }
            set { ViewState["IconsFolder"] = value; }
        }

        #endregion

        #region [ Methods ]

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1055:UriReturnValuesShouldNotBeStrings")]
        protected string LocalResolveUrl(string path)
        {
            string temp = base.ResolveUrl(path);
            Regex _Regex = new Regex(@"(\(S\([A-Za-z0-9_]+\)\)/)", RegexOptions.Compiled);
            temp = _Regex.Replace(temp, "");
            return temp;
        }

        protected override void CreateChildControls()
        {
            Table table = new Table();
            TableRow row = null;
            TableCell cell;

            string iconsFolder = LocalResolveUrl(this.IconsFolder);
            if (iconsFolder.Length > 0)
            {
                string lastCh = iconsFolder.Substring(iconsFolder.Length - 1, 1);
                if (lastCh != "\\" && lastCh != "/") iconsFolder += "/";
            }

            if (Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(iconsFolder)))
            {
                string[] files = Directory.GetFiles(System.Web.HttpContext.Current.Server.MapPath(iconsFolder));
                int j = 0;

                foreach (string file in files)
                {
                    string ext = Path.GetExtension(file).ToLower();
                    if (ext == ".gif" || ext == ".jpg" || ext == ".jpeg" || ext == ".png")
                    {
                        if (j == 0)
                        {
                            row = new TableRow();
                            table.Rows.Add(row);
                        }
                        cell = new TableCell();
                        System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                        image.ImageUrl = iconsFolder + Path.GetFileName(file);
                        image.Attributes.Add("onmousedown", "insertImage(\"" + iconsFolder + Path.GetFileName(file) + "\")");
                        image.Style[HtmlTextWriterStyle.Cursor] = "pointer";
                        cell.Controls.Add(image);
                        row.Cells.Add(cell);

                        j++;
                        if (j == IconsInRow) j = 0;
                    }
                }
            }
            table.Attributes.Add("border", "0");
            table.Attributes.Add("cellspacing", "2");
            table.Attributes.Add("cellpadding", "0");
            table.Style["background-color"] = "transparent";

            Content.Add(table);

            base.CreateChildControls();
        }

        #endregion
    }
}

namespace SageFrameAJaxEditorControls
{
    /// <summary>
    /// SageFrame ajax editor class.
    /// </summary>
    public class SageFrameAJaxEditor : Editor
    {
        protected override void FillTopToolbar()
        {
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
        }

        protected override void FillBottomToolbar()
        {
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
        }
    }

    /// <summary>
    /// SageFrame custom ajax editor class.
    /// </summary>
    public class SageFrameAJaxEditorCustom : Editor
    {
        protected override void FillTopToolbar()
        {
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Copy());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Paragraph());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Paste());
        }

        protected override void FillBottomToolbar()
        {
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
        }
    }
    //}

    //namespace SageFrameAJaxEditorControls
    //{
    /// <summary>
    /// SageFrame lite class.
    /// </summary>
    public class Lite : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillBottomToolbar()
        {
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
        }

        protected override void FillTopToolbar()
        {
            Collection<AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption> options;
            AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption option;

            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Underline());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());

            AjaxControlToolkit.HTMLEditor.ToolbarButton.FixedForeColor FixedForeColor = new AjaxControlToolkit.HTMLEditor.ToolbarButton.FixedForeColor();
            TopToolbar.Buttons.Add(FixedForeColor);
            AjaxControlToolkit.HTMLEditor.ToolbarButton.ForeColorSelector ForeColorSelector = new AjaxControlToolkit.HTMLEditor.ToolbarButton.ForeColorSelector();
            ForeColorSelector.FixedColorButtonId = FixedForeColor.ID = "FixedForeColor";
            TopToolbar.Buttons.Add(ForeColorSelector);
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.ForeColorClear());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());

            AjaxControlToolkit.HTMLEditor.ToolbarButton.FontName fontName = new AjaxControlToolkit.HTMLEditor.ToolbarButton.FontName();
            TopToolbar.Buttons.Add(fontName);

            options = fontName.Options;
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "arial,helvetica,sans-serif";
            option.Text = "Arial";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "courier new,courier,monospace";
            option.Text = "Courier New";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "georgia,times new roman,times,serif";
            option.Text = "Georgia";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "";
            option.Text = "Tahoma";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "times new roman,times,serif";
            option.Text = "";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "";
            option.Text = "Verdana";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "impact";
            option.Text = "Impact";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "wingdings";
            option.Text = "WingDings";
            options.Add(option);

            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());
            AjaxControlToolkit.HTMLEditor.ToolbarButton.FontSize fontSize = new AjaxControlToolkit.HTMLEditor.ToolbarButton.FontSize();
            TopToolbar.Buttons.Add(fontSize);

            options = fontSize.Options;
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "8pt";
            option.Text = "1 ( 8 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "10pt";
            option.Text = "2 (10 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "12pt";
            option.Text = "3 (12 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "14pt";
            option.Text = "4 (14 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "18pt";
            option.Text = "5 (18 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "24pt";
            option.Text = "6 (24 pt)";
            options.Add(option);
            option = new AjaxControlToolkit.HTMLEditor.ToolbarButton.SelectOption();
            option.Value = "36pt";
            option.Text = "7 (36 pt)";
            options.Add(option);

            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.RemoveStyles());
        }
    }

    /// <summary>
    /// Class that inherits Lite and fills element at bottom.
    /// </summary>
    public class LiteNoBottom : Lite
    {
        protected override void FillBottomToolbar()
        {
        }
    }

    /// <summary>
    /// Class that inherits AjaxControlToolkit.HTMLEditor.Editor and fills element at bottom.
    /// </summary>
    public class FullNoBottom : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillBottomToolbar()
        {
        }
    }

    /// <summary>
    /// Class that inherits AjaxControlToolkit.HTMLEditor.Editor and overrides FillBottomToolbar method to fills element at bottom.
    /// </summary>
    public class FullWithRightBottom : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillBottomToolbar()
        {
            // reverse
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
        }
    }

    /// <summary>
    /// Class that inherits AjaxControlToolkit.HTMLEditor.Editor and overrides FillBottomToolbar method to fills element at bottom.
    /// </summary>
    public class EditorWithCustomButtons : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillTopToolbar()
        {
            base.FillTopToolbar();
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.CustomToolbarButton.InsertDate());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.CustomToolbarButton.InsertIcon());
        }

        /// <summary>
        /// Returns button's image folder.
        /// </summary>
        public override string ButtonImagesFolder
        {
            get
            {
                return "~/App_Images/HTMLEditor.customButtons/";
            }
        }
    }

    /// <summary>
    /// Editor with custom emotions buttons class.
    /// </summary>
    public class EditorWithCustomEmotionsButtons : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillTopToolbar()
        {
            //base.FillTopToolbar();
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.InsertLink());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.ForeColor());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Underline());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.OrderedList());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.BulletedList());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HorizontalSeparator());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.CustomToolbarButton.InsertIcon());
        }

        /// <summary>
        /// Returns button image folder.
        /// </summary>
        public override string ButtonImagesFolder
        {
            get
            {
                return "~/App_Images/HTMLEditor.customButtons/";
            }
        }

        protected override void FillBottomToolbar()
        {
            // reverse
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
            //BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
        }
    }


    /// <summary>
    /// Editor without custom emotions buttons
    /// </summary>
    public class EditorWithoutCustomEmotionsButtons : AjaxControlToolkit.HTMLEditor.Editor
    {
        protected override void FillTopToolbar()
        {
            //base.FillTopToolbar();
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.InsertLink());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.ForeColor());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Bold());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Italic());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.Underline());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.OrderedList());
            TopToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.BulletedList());

        }

        /// <summary>
        /// Returns button's image folder.
        /// </summary>
        public override string ButtonImagesFolder
        {
            get
            {
                return "~/App_Images/HTMLEditor.customButtons/";
            }
        }

        protected override void FillBottomToolbar()
        {
            // reverse
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.PreviewMode());
            //BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.HtmlMode());
            BottomToolbar.Buttons.Add(new AjaxControlToolkit.HTMLEditor.ToolbarButton.DesignMode());
        }
    }
}
