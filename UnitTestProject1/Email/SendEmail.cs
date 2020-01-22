using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FPAPIAutomation_MSTests.Email
{
    class SendEmail
    {
        public void Email()
        {

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            var fromAddress = new MailAddress("testautomation@ocean.com.au", "Test Automation");
            var toAddress = new MailAddress("anupd@ocean.com.au");


            string fromPassword = "fromPassword";
            string subject = "Dry Run: FlightProWeb Test Execution Report (Smoke Test) - " + DateTime.Now;

            AlternateView av1 = AlternateView.CreateAlternateViewFromString("<p><em>[You are receiving this email as you have been identified as a stakeholder for Test Automation Email reports]</em></p><p>&nbsp;Hi all,&nbsp;</p><p> The Test Automation(FlightPro) Web Smoke Test Execution has run at " + DateTime.Now + " and the below Dashboard has been updated; kindly refresh the browser to get the latest updated results.</p><p>&nbsp;</p><p><a href=\"http://10.0.2.35:8080/FPWebAutomation/Report/Report.html \">http://10.0.2.35:8080/FPWebAutomation/Report/Report.html</a></p><p>&nbsp;</p><p><span style = \"font-size: 10.0pt;\" > Please note the above link works if you are connected to Ocean network, In case you are not connected to Ocean Network there is a report attached to this email.</span></p><p><span style = \"font-size: 10.0pt;\" > This is an automatically generated email &ndash; but this email accepts replies and is monitored.</span></p><p>&nbsp;</p><p>Regards,</p><p>&nbsp;</p><p><strong><span style = \"font-size: 12.0pt; font-family: 'Arial',sans-serif; color: #003c71;\"> Test Automation </span></strong></p><p><strong> &nbsp;</strong></p><p><strong><span style = \"font-size: 9.0pt; font-family: 'Arial',sans-serif; color: #003c71;\" >&nbsp;<span style = \"font-family: 'Arial',sans-serif;\">Ocean Software Pty Ltd </span></strong></p><p><strong><span style = \"font-size: 2.0pt; font-family: 'Arial',sans-serif; color: #003c71;\">&nbsp;<img src=\"https://www.ocean.software/wp-content/uploads/2017/02/ocean-software-logo.png\" alt =\"ocean.software\">;</span></ strong ></p><p><span style = \"font-size: 9.0pt; font-family: 'Arial',sans-serif; color: #003c71;\"> Level 40, 120 Collins Street</span></p><p><span style = \"font-size: 9.0pt; font-family: 'Arial',sans-serif; color: #003c71;\"> Melbourne, VIC, 3000, Australia </span></p><p><u><span style = \"color: #00aeef;\"><a href = \"http://www.ocean.software/\"> www.ocean.software </a></span></u></p> ", null, "text/html");


            var smtp = new SmtpClient
            {
                Host = "10.0.2.35",
                Port = 25,
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)

            };
            using (var message = new System.Net.Mail.MailMessage(fromAddress, toAddress)
            {
                Subject = subject,




            })


            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment("C:/FlightPro/Dev/_main/Test Automation/FlightPro/FightPro_WebAutomation/FPWebAutomation/Report/Report.html");


                /*
                av1.LinkedResources.Add(
                      new LinkedResource(@"C:/Users/anupd/Documents/OceanLogo.png")
                      {
                          ContentId = "companylogo",
                          TransferEncoding = TransferEncoding.Base64,
                          ContentType = new ContentType("image/png")
                      });
                      */
                attachment.Name = "FPAPIAutomationTests.html";
                message.To.Add("anupd@ocean.com.au");
                //  message.To.Add("anithaj@ocean.com.au");
                //  message.To.Add("shabnams@ocean.com.au");
                //  message.To.Add("bens@ocean.com.au");
                //  message.To.Add("amarp@ocean.com.au");
                message.Attachments.Add(attachment);
                message.AlternateViews.Add(av1);
                message.IsBodyHtml = true;
                smtp.Send(message);

            }


        }


    }
}
