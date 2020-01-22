using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mail;

namespace ClassLibrary1
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
