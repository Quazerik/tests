using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Net.Mail;

namespace TestsApp
{
    class Email
    {
        public static async Task SendLoginAndPassword(string email, string fullName, string password)
        {
            // Всегда переписывается на email SMTP.
            MailAddress from = new MailAddress("IsNotImportant@gmail.com", "TestsApp");
            MailAddress to = new MailAddress(email, fullName);
            MailMessage mail = new MailMessage(from, to)
            {
                Subject = "Registration Info",
                Body = "Hi!\nYou have successfully registered in TestsApp.\nYour login: " + email + "\nYour password: " + password
            };

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                // Gmail smtp.
                Credentials = new NetworkCredential("itis.kfu.11.606@gmail.com", "itiskfu11606"),
                EnableSsl = true
            };

            await smtp.SendMailAsync(mail);
        }
    }
}