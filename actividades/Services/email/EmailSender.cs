using System.Net.Mail;
using actividades.Models;
using MailKit.Net.Smtp;
using MimeKit;

public class EmailSender
{
    public async Task SendTaskNotificationEmail(Tarea tarea, string userEmail)
    {
        Console.WriteLine("correo"+userEmail);
        try
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Actividades", "noreplyactividades.com"));
            message.To.Add(new MailboxAddress(userEmail, userEmail));
            message.Subject = "Tarea próxima a vencer";

            var builder = new BodyBuilder();
            builder.TextBody = $"La tarea '{tarea.Nombre}' está próxima a vencer el {tarea.FechaLimite?.ToString("dd/MM/yyyy")}.";
            builder.HtmlBody = $"La tarea '{tarea.Nombre}' está próxima a vencer el {tarea.FechaLimite?.ToString("dd/MM/yyyy")}.";
            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false); // Use your SMTP server and port
                client.Authenticate("miltonnavas7@gmail.com", "pzvk adcl exni owpq"); // Use your SMTP username and password
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("errorrrrr");
            // Manejo de errores
        }
    }
}
