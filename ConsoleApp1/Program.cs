using System;
using System.Net;
using System.Net.Mail;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            void main()
            {

                var fromAddress = new MailAddress("testautomation@oceam.com.au", "From Name");
                var toAddress = new MailAddress("anupd@ocean.com.au", "To Name");
                string fromPassword = "fromPassword";
                string subject = "Subject";
                string body = "Body";

                var smtp = new SmtpClient
                {
                    Host = "10.0.2.35",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }




            }
        }
    }
}