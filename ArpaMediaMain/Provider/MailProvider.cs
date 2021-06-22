using ArpaMedia.Web.Api.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.IO;
using System.Threading.Tasks;
using ArpaMedia.Data.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace ArpaMedia.Web.Api.Entity.EntityServices
{
    public class MailProvider
    {
        /// <summary>
        /// Send post to all subscribers
        /// </summary>
        /// <param name="post">Data of the post.</param> 
        /// <param name="configuration">Settings of the Smtp Server.</param>  
        public async Task SendEmailsToSubscribersForPost(PostResponse post, IConfiguration configuration)
        {
            SubscribeService subscribeService = new SubscribeService();
            List<Subscribed> subscribes = subscribeService.GetSubscribers();

            if (subscribes == null || subscribes.Count == 0)
            {
                return;
            }

            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(configuration["MailSettings:Mail"]);
            foreach (var subsribe in subscribes)
            {  
                email.To.Add(MailboxAddress.Parse(subsribe.Email));
            }

            var finalPathForHtml = Path.Combine(Directory.GetCurrentDirectory(), @"Views\Templates\EmailTemplate.html");

            string htmlString = "";
            using (StreamReader reader = new StreamReader(finalPathForHtml))
            {
                htmlString = reader.ReadToEnd();
            }

            htmlString = htmlString.Replace("{POST_TITLE}", post.Title);
            htmlString = htmlString.Replace("{POST_DESCRIPTION}", post.Description);
            htmlString = htmlString.Replace("{POST_IMAGE_FULL_URL}", post.PostImage);

            var builder = new BodyBuilder();
            builder.HtmlBody = htmlString;

            email.Body = builder.ToMessageBody();
            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            var host = configuration["MailSettings:Host"];
            var port = int.Parse(configuration["MailSettings:Port"]);
            var mail = configuration["MailSettings:Mail"];
            var password = configuration["MailSettings:Password"];

            smtp.Connect(host, port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mail, password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
