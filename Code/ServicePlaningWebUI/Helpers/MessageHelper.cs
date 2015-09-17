using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Services.Description;
using MailMessage = System.Net.Mail.MailMessage;
using MailPriority = System.Net.Mail.MailPriority;

namespace ServicePlaningWebUI.Helpers
{
    public class MessageHelper
    {
        public class Email
        {
            #region constructor
            public Email(string appPath)
            {
                //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var config = WebConfigurationManager.OpenWebConfiguration(appPath);
                var mailSettings = config.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;
                _smtpmailserver = mailSettings.Smtp.Network.Host;
                _portnumber = mailSettings.Smtp.Network.Port;
                _fromAddress = mailSettings.Smtp.From;
            }
            #endregion constructor

            private string _smtpmailserver = "";
            private string _fromAddress = "";
            private int _portnumber = 25;

            public void SendMail(string toAddress, string subject, string text)
            {
                var smtp = new SmtpClient();
                smtp.Host = _smtpmailserver;
                smtp.Port = _portnumber;
                //smtp.EnableSsl = true;
                //smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                //smtp.Credentials = new NetworkCredential("", "");
                smtp.Timeout = 20000;

                text = text.Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");

                MailMessage msg = new MailMessage(_fromAddress, toAddress, subject, text);
                msg.IsBodyHtml = true;

                smtp.Send(msg);
            }

        }
    }
}