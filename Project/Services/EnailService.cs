using Project.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
namespace Project.Services
{
 public class EmailSender: IEmailSender
 {
  public Task SendEmailAsync(string email,string subject, string message)
  {
   var client = new SmtpClient("smtp.gmail.com", 587)
   {
    EnableSsl = true,
    UseDefaultCredentials =false,
    Credentials = new NetworkCredential("nattila2001@gmail.com", "ylhc zlir tyov yzip")
   };
   return client.SendMailAsync(
    new MailMessage(from: "nattila2001@gmail.com",
    to: "alternativeuser7@gmail.com",
    subject,
    message));
  }
 }
}
