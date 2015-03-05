using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskLogger.Data.Services
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;

    using Microsoft.AspNet.Identity;

    using SendGrid;

    public class EmailService: IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendgridAsync(message);
        }

        private async Task ConfigSendgridAsync(IdentityMessage message)
        {
            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress("milenppavlov@gmail.com", "Task Logger");
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(ConfigurationManager.AppSettings["emailService:Account"],
                                                ConfigurationManager.AppSettings["emailService:Password"]);

            var transportWeb = new Web(credentials);

            if (transportWeb != null)
            {
                await transportWeb.DeliverAsync(myMessage);
            }
            else
            {
                await Task.FromResult(0);
            }

        }
    }
}
