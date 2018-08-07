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

namespace RemoteUpkeep.EmailEngine
{
    public static class EmailHelper
    {
        public static string GetHtmlBody(EmailViewModel model, string body, string subject, string logo = null)
        {
            //string body = Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(EmailViewModel), model);

            EmailLayoutViewModel layoutModel = new EmailLayoutViewModel
            {
                Subject = subject,
                Body = body.TextToHtml(),
                Logo = logo,
                Link = Properties.Resources.Link,
                Year = DateTime.Now.Year.ToString(),
                Copyright = Properties.Resources.Copyright,
                ContactUsLink = Properties.Resources.ContactUsLink,
                ContactUs = Properties.Resources.ContactUs
            };

            return Engine.Razor.RunCompile(File.ReadAllText(HttpContext.Current.Server.MapPath("~/Views/Shared/Email_layout.html")), Guid.NewGuid().ToString(), typeof(EmailLayoutViewModel), layoutModel);
        }

        public static string GetTextBody(EmailViewModel model, string bodyTemplate)
        {
            return Engine.Razor.RunCompile(bodyTemplate, Guid.NewGuid().ToString(), typeof(EmailViewModel), model).HtmlToText();
        }

        public static void SendEmail(EmailViewModel model, string bodyTemplate, string subject)
        {
            //replace body tokens
            var config = new TemplateServiceConfiguration();
            var service = RazorEngineService.Create(config);
            Engine.Razor = service;

            //send email

            SmtpClient client = new SmtpClient();
            //client.UseDefaultCredentials = false;
            //client.Host = "mail.remote-upkeep.com.ua";
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //System.Net.NetworkCredential networkCred = new System.Net.NetworkCredential();
            //networkCred.UserName = "noreply@remote-upkeep.com.ua";
            //networkCred.Password = "";
            //client.Credentials = networkCred;

            //client.Port = 465;
            //client.EnableSsl = true;
            //client.Timeout = 500000;

            MailAddress from = model.Sender == null || model.Sender.Email == model.Receiver.Email ?
                new MailAddress(Properties.Resources.EmailNoReply, Properties.Resources.EmailNoReplyName, System.Text.Encoding.UTF8) :
                new MailAddress(model.Sender.Email, model.Sender.FullName, System.Text.Encoding.UTF8);

            MailAddress to = new MailAddress(model.Receiver.Email, model.Receiver.FullName, System.Text.Encoding.UTF8);

            MailMessage message = new MailMessage(from, to);
            message.Subject = subject;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            //var logo = new LinkedResource(HttpContext.Current.Server.MapPath("~/Content/images/logo_email.png"));
            //logo.ContentId = Guid.NewGuid().ToString();

            message.Body = GetTextBody(model, bodyTemplate);

            string body = GetHtmlBody(model, bodyTemplate, subject);// logo.ContentId);

            var view = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, "text/html");
            //view.LinkedResources.Add(logo);
            message.AlternateViews.Add(view);

            client.Send(message);
        }

        public static void SendEmail(ApplicationUser receiver, string bodyTemplate, string subject)
        {
            SendEmail(new EmailViewModel { Receiver = receiver }, bodyTemplate, subject);
        }

        public static void SendConfirmEmail(ApplicationUser user, string callbackUrl)
        {
            string body = "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>";
            SendEmail(user, body, "Confirm your account");
        }

    }
}