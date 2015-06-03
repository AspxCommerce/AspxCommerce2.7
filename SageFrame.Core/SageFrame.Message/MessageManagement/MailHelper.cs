#region "Copyright"

/*
FOR FURTHER DETAILS ABOUT LICENSING, PLEASE VISIT "LICENSE.txt" INSIDE THE SAGEFRAME FOLDER
*/

#endregion

#region "References"

using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.IO;
using System.Security.Principal;
using System.Web;
using System.Net.Mail;
using SageFrame.Web;

#endregion



namespace SageFrame.SageFrameClass.MessageManagement
{
    /// <summary>
    ///  MailHelper class consists of all the mail sending  methods along with attachments.
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Sends multiple email.
        /// </summary>
        /// <param name="From">Email sending from.</param>
        /// <param name="sendTo">Email sending to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        public static void SendMultipleEmail(string From, string sendTo, string Subject, string Body)
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string ServerPort = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPServer);
            string SMTPAuthentication = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPAuthentication);
            string SMTPEnableSSL = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPEnableSSL);
            string SMTPPassword = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPPassword);
            string SMTPUsername = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPUsername);
            string[] SMTPServer = ServerPort.Split(':');
            try
            {
                MailMessage myMessage = new MailMessage();
                foreach (string emailTo in sendTo.Split(','))
                {
                    myMessage.To.Add(new MailAddress(emailTo));
                }
                myMessage.From = new MailAddress(From);
                myMessage.Subject = Subject;
                myMessage.Body = Body;
                myMessage.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                if (SMTPAuthentication == "1")
                {
                    if (SMTPUsername.Length > 0 && SMTPPassword.Length > 0)
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                    }
                }
                smtp.EnableSsl = bool.Parse(SMTPEnableSSL.ToString());
                if (SMTPServer.Length > 0)
                {
                    if (SMTPServer[0].Length != 0)
                    {
                        smtp.Host = SMTPServer[0];
                        if (SMTPServer.Length == 2)
                        {
                            smtp.Port = int.Parse(SMTPServer[1]);
                        }
                        else
                        {
                            smtp.Port = 25;
                        }
                        smtp.Send(myMessage);
                    }
                    else
                    {
                        throw new Exception("SMTP Host must be provided");
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sends multiple emails.
        /// </summary>
        /// <param name="From">Email sending from.</param>
        /// <param name="sendTo">Email sending to</param>
        /// <param name="Subject">Email's subject</param>
        /// <param name="Body">Email's body</param>
        /// <param name="Identifiers">Email's identifiers</param>
        /// <param name="pageName">Page name</param>
        public static void SendMultipleEmail(string From, string sendTo, string Subject, string Body, string Identifiers, string pageName)
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string ServerPort = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPServer);
            string SMTPAuthentication = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPAuthentication);
            string SMTPEnableSSL = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPEnableSSL);
            string SMTPPassword = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPPassword);
            string SMTPUsername = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPUsername);
            string[] SMTPServer = ServerPort.Split(':');
            try
            {
                MailMessage myMessage = new MailMessage();
                List<string> lstTo = new List<string>();
                List<string> lstidentity = new List<string>();
                foreach (string emailTo in sendTo.Split(','))
                {
                    lstTo.Add(emailTo);
                    myMessage.To.Add(new MailAddress(emailTo));
                }
                foreach (string identity in Identifiers.Split(','))
                {
                    lstidentity.Add(identity);
                }


                SmtpClient smtp = new SmtpClient();
                if (SMTPAuthentication == "1")
                {
                    if (SMTPUsername.Length > 0 && SMTPPassword.Length > 0)
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                    }
                }
                smtp.EnableSsl = bool.Parse(SMTPEnableSSL.ToString());
                if (SMTPServer.Length > 0)
                {
                    if (SMTPServer[0].Length != 0)
                    {
                        smtp.Host = SMTPServer[0];
                        if (SMTPServer.Length == 2)
                        {
                            smtp.Port = int.Parse(SMTPServer[1]);
                        }
                        else
                        {
                            smtp.Port = 25;
                        }
                        int length = lstidentity.Count;
                        for (int j = 0; j < length; j++)
                        {
                            try
                            {
                                myMessage.From = new MailAddress(From);
                                myMessage.To.Add(lstTo[j]);
                                myMessage.Subject = Subject;
                                myMessage.Body = Body + "<br/><br/>if you want to unsubscribe click the link below <br/> " + pageName + "?id=" + lstidentity[j];
                                myMessage.IsBodyHtml = true;
                                smtp.Send(myMessage);
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("SMTP Host must be provided");
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sends mail with no attachment.
        /// </summary>
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="CC">Email CC to</param>
        /// <param name="BCC">EMail BCC to</param>
        public static void SendMailNoAttachment(string From, string sendTo, string Subject, string Body, string CC,
                                                string BCC)
        {
            SendEMail(From, sendTo, Subject, Body, CC, BCC);
        }

        /// <summary>
        /// Sends mail with one attachment
        /// </summary>       
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="AttachmentFile">Attachment file path.</param>
        /// <param name="CC">Email CC to.</param>
        /// <param name="BCC">EMail BCC to.</param>
        public static void SendMailOneAttachment(string From, string sendTo, string Subject, string Body,
                                                 string AttachmentFile, string CC, string BCC)
        {
            SendEMail(From, sendTo, Subject, Body, AttachmentFile, CC, BCC);
        }


        /// <summary>
        /// Sends mail with multiple attachment files
        /// </summary>       
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="AttachmentFiles">Array of attachment files path.</param>
        /// <param name="CC">Email CC to.</param>
        /// <param name="BCC">EMail BCC to.</param>
        public static void SendMailMultipleAttachments(string From, string sendTo, string Subject, string Body,
                                                       ArrayList AttachmentFiles, string CC, string BCC)
        {
            SendEMail(From, sendTo, Subject, Body, AttachmentFiles, CC, BCC);
        }

        /// <summary>
        /// Sends mail.
        /// </summary>
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="CC">Email CC to</param>
        /// <param name="BCC">EMail BCC to</param>
        public static void SendEMail(string From, string sendTo, string Subject, string Body, string CC, string BCC)
        {
            ArrayList AttachmentFiles;
            AttachmentFiles = null;
            SendEMail(From, sendTo, Subject, Body, AttachmentFiles, CC, BCC);
        }


        /// <summary>
        /// Sends mail with attachment
        /// </summary>
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="AttachmentFile"></param>
        /// <param name="CC">Email CC to</param>
        /// <param name="BCC">EMail BCC to</param>
        public static void SendEMail(string From, string sendTo, string Subject, string Body, string AttachmentFile,
                                     string CC, string BCC)
        {
            ArrayList AttachmentFiles = new ArrayList();
            if (AttachmentFile != null && AttachmentFile.Length != 0)
            {
                AttachmentFiles.Add(AttachmentFile);
            }
            else
            {
                AttachmentFiles = null;
            }
            SendEMail(From, sendTo, Subject, Body, AttachmentFiles, CC, BCC);
        }

        /// <summary>
        /// Sends mail with multiple attachment files
        /// </summary>       
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="AttachmentFiles">Array of attachment files path.</param>
        /// <param name="CC">Email CC to.</param>
        /// <param name="BCC">EMail BCC to.</param>
        public static void SendEMail(string From, string sendTo, string Subject, string Body, ArrayList AttachmentFiles,
                                     string CC, string BCC)
        {
            SendEMail(From, sendTo, Subject, Body, AttachmentFiles, CC, BCC, true);
        }


        /// <summary>
        /// Sends mail with multiple attachment files
        /// </summary>       
        /// <param name="From">Email from.</param>
        /// <param name="sendTo">Email to.</param>
        /// <param name="Subject">Email's subject.</param>
        /// <param name="Body">Email's body.</param>
        /// <param name="AttachmentFiles">Array of attachment files path.</param>
        /// <param name="CC">Email CC to.</param>
        /// <param name="BCC">EMail BCC to.</param>
        /// <param name="IsHtmlFormat">Set true if the body must be in HTML format.</param>
        public static void SendEMail(string From, string sendTo, string Subject, string Body, ArrayList AttachmentFiles, string CC, string BCC, bool IsHtmlFormat)
        {
            SageFrameConfig sfConfig = new SageFrameConfig();
            string ServerPort = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPServer);
            string SMTPAuthentication = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPAuthentication);
            string SMTPEnableSSL = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPEnableSSL);
            string SMTPPassword = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPPassword);
            string SMTPUsername = sfConfig.GetSettingValueByIndividualKey(SageFrameSettingKeys.SMTPUsername);
            string[] SMTPServer = ServerPort.Split(':');
            try
            {
                MailMessage myMessage = new MailMessage();
                myMessage.To.Add(sendTo);
                myMessage.From = new MailAddress(From);
                myMessage.Subject = Subject;
                myMessage.Body = Body;
                myMessage.IsBodyHtml = true;

                if (CC.Length != 0)
                    myMessage.CC.Add(CC);

                if (BCC.Length != 0)
                    myMessage.Bcc.Add(BCC);

                if (AttachmentFiles != null)
                {
                    foreach (string x in AttachmentFiles)
                    {
                        if (File.Exists(x)) myMessage.Attachments.Add(new Attachment(x));
                    }
                }
                SmtpClient smtp = new SmtpClient();
                if (SMTPAuthentication == "1")
                {
                    if (SMTPUsername.Length > 0 && SMTPPassword.Length > 0)
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(SMTPUsername, SMTPPassword);
                    }
                }
                smtp.EnableSsl = bool.Parse(SMTPEnableSSL.ToString());
                if (SMTPServer.Length > 0)
                {
                    if (SMTPServer[0].Length != 0)
                    {
                        smtp.Host = SMTPServer[0];
                        if (SMTPServer.Length == 2)
                        {
                            smtp.Port = int.Parse(SMTPServer[1]);
                        }
                        else
                        {
                            smtp.Port = 25;
                        }
                        smtp.Send(myMessage);
                    }
                    else
                    {
                        throw new Exception("SMTP Host must be provided");
                    }
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Formats email into javascript file
        /// </summary>
        /// <param name="email">Email to be formatted</param>
        /// <returns>Scripted email.</returns>
        public static string FormatEmail(string email)
        {
            string user = email.Substring(0, email.IndexOf('@'));
            string dom = email.Substring(email.IndexOf('@') + 1);
            return "<script language=\"javascript\">var name = \"" + user + "\"; var domain = \"" + dom + "\"; document.write('<a href=\"mailto:' + name + String.fromCharCode(64) + domain + '\">' + name + String.fromCharCode(64) + domain + '</a>')</script><noscript>" + user + " at " + dom + "</noscript>";
        }
    }
}