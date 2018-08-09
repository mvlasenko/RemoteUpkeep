using System;
using System.Net.Mail;
using System.Web;
using RemoteUpkeep.Helpers;
using RemoteUpkeep.Models;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RemoteUpkeep.ViewModels;
using System.IO;
using RemoteUpkeep.Properties;

namespace RemoteUpkeep.EmailEngine
{
    public static class EmailHelper
    {
        public static string GetHtmlBody(string body, string subject, string logo = null)
        {
            EmailLayoutViewModel layoutModel = new EmailLayoutViewModel
            {
                Subject = subject,
                Body = body.TextToHtml(),
                Logo = logo,
                Link = Resources.Link,
                Year = DateTime.Now.Year.ToString(),
                Copyright = Resources.Copyright,
                ContactUsLink = Resources.ContactUsLink,
                ContactUs = Resources.ContactUs
            };

            return Engine.Razor.RunCompile(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Shared/Email_layout.html")), Guid.NewGuid().ToString(), typeof(EmailLayoutViewModel), layoutModel);
        }

        public static string GetTextBody(string body)
        {
            return body.HtmlToText();
        }

        public static string GetFormattedBody(EmailViewModel model)
        {
            string template = Resources.EmailBodyTemplate;
            return Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), typeof(EmailViewModel), model);
        }

        public static string GetFormattedBody(ApplicationUser receiver, string body)
        {
            return GetFormattedBody(new EmailViewModel { Receiver = receiver, Body = body, SignatureName = Resources.SiteName });
        }

        public static string GetUserFormattedBody(ApplicationUser user, string template)
        {
            return Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), typeof(ApplicationUser), user);
        }

        public static void SendEmail(EmailViewModel model, string subject)
        {
            //replace body tokens
            var config = new TemplateServiceConfiguration();
            var service = RazorEngineService.Create(config);
            Engine.Razor = service;

            //send email

            SmtpClient client = new SmtpClient();

            MailAddress from = model.Sender == null || model.Sender.Email == model.Receiver.Email ?
                new MailAddress(Resources.EmailNoReply, Resources.EmailNoReplyName, System.Text.Encoding.UTF8) :
                new MailAddress(model.Sender.Email, model.Sender.FullName, System.Text.Encoding.UTF8);

            MailAddress to = new MailAddress(model.Receiver.Email, model.Receiver.FullName, System.Text.Encoding.UTF8);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            //var logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/images/logo_email.png"));
            //logo.ContentId = Guid.NewGuid().ToString();

            string formattedBody = GetFormattedBody(model);

            message.Body = GetTextBody(formattedBody);

            string htmlBody = GetHtmlBody(formattedBody, subject);// logo.ContentId);

            var view = AlternateView.CreateAlternateViewFromString(htmlBody, System.Text.Encoding.UTF8, "text/html");
            //view.LinkedResources.Add(logo);
            message.AlternateViews.Add(view);

            client.Send(message);
        }

        public static void SendEmail(ApplicationUser receiver, string body, string subject)
        {
            SendEmail(new EmailViewModel { Receiver = receiver, Body = body, SignatureName = Resources.SiteName }, subject);
        }

        public static void SendAdminEmail(string body, string subject)
        {
            SendEmail(new ApplicationUser { Email = Resources.AdminEmail, FirstName = Resources.AdminName }, body, subject);
        }
    }
}