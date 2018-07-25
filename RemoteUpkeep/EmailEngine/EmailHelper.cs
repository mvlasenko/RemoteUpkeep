using System;
using System.Net.Mail;
using System.Web;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RemoteUpkeep.ViewModels;
using RemoteUpkeep.Properties;
using System.IO;

namespace RemoteUpkeep.EmailEngine
{
    public static class EmailHelper
    {
        public static string GetHtmlBody(EmailViewModel model, string bodyTemplate, string subject, string logo = null)
        {
            string body = Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(EmailViewModel), model);

            EmailLayoutViewModel layoutModel = new EmailLayoutViewModel
            {
                Subject = subject,
                Body = body,
                Logo = logo,
                Link = Resources.Link,
                Year = DateTime.Now.Year.ToString(),
                Copyright = Resources.Copyright,
                ContactUsLink = Resources.ContactUsLink,
                ContactUs = Resources.ContactUs
            };

            return Engine.Razor.RunCompile(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Shared/Email_layout.html")), Guid.NewGuid().ToString(), typeof(EmailLayoutViewModel), layoutModel);
        }

        public static string GetTextBody(EmailViewModel model, string bodyTemplate)
        {
            return Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(EmailViewModel), model).HtmlToText();
        }

        public static void SendEmail(EmailViewModel model, string bodyTemplate, string subject)
        {
            try
            {
                //replace body tokens
                var config = new TemplateServiceConfiguration();
                var service = RazorEngineService.Create(config);
                Engine.Razor = service;

                //send email

                SmtpClient client = new SmtpClient();

                MailAddress from = model.Sender != null ?
                    new MailAddress(model.Sender.Email, model.Sender.FullName, System.Text.Encoding.UTF8) :
                    new MailAddress(Resources.EmailNoReply, Resources.EmailNoReplyName, System.Text.Encoding.UTF8);

                MailAddress to = new MailAddress(model.Receiver.Email, model.Receiver.FullName, System.Text.Encoding.UTF8);

                MailMessage message = new MailMessage(from, to);
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.IsBodyHtml = true;

                var logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/images/logo_email.png"));
                logo.ContentId = Guid.NewGuid().ToString();

                message.Body = GetTextBody(model, bodyTemplate);

                string body = GetHtmlBody(model, bodyTemplate, subject, logo.ContentId);

                var view = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, "text/html");
                view.LinkedResources.Add(logo);
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

        public static void SendEmail(ApplicationUser user, string bodyTemplate, string subject)
        {
            SendEmail(new EmailViewModel { Receiver = user }, bodyTemplate, subject);
        }

        public static void SendConfirmEmail(ApplicationUser user, string callbackUrl)
        {
            string body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
            SendEmail(user, body, "Confirm your account");
        }

    }
}