using System;
using System.Net.Mail;
using System.Web;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace RemoteUpkeep.EmailEngine
{
    public static class EmailHelper
    {
        public static string GetEmailBody(ApplicationUser recipient, string bodyTemplate, string subject, string logoTop = null)
        {
            return Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(ApplicationUser), recipient);
        }

        public static string GetTextBody(ApplicationUser recipient, string bodyTemplate)
        {
            return Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(ApplicationUser), recipient).HtmlToText();
        }

        public static void SendEmail(string fromEmail, ApplicationUser recipient, string bodyTemplate, string subject)
        {
            try
            {
                //replace body tokens
                var config = new TemplateServiceConfiguration();
                var service = RazorEngineService.Create(config);
                Engine.Razor = service;

                //send email
                SmtpClient client = new SmtpClient();

                MailAddress from = new MailAddress(fromEmail);
                MailAddress to = new MailAddress(recipient.Email, recipient.FirstName + " " + recipient.LastName, System.Text.Encoding.UTF8);

                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                var logoTop = new LinkedResource(HttpContext.Current.Server.MapPath("~/Images/logo_top.png"));
                logoTop.ContentId = Guid.NewGuid().ToString();

                message.Body = GetTextBody(recipient, bodyTemplate);

                string body = GetEmailBody(recipient, bodyTemplate, subject, logoTop.ContentId);

                var view = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, "text/html");
                view.LinkedResources.Add(logoTop);
                message.AlternateViews.Add(view);

                client.Send(message);
            }
            catch (Exception ex)
            {
                //todo: configure logging for odata application
                //SiteLogger.Error("Exception occurred in {2}: {0}\r\n{1}", LoggingCategory.General, ex.Message, ex.StackTrace, typeof(EmailHelper).Name);
                //throw;
            }
        }

        public static void SendConfirmEmail(ApplicationUser user, string callbackUrl)
        {
            string body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
            SendEmail(user.Email, user, body, "Confirm your account");
        }

    }
}