using Quartz;
using actividades.Models;
using Microsoft.EntityFrameworkCore;

public class TaskReminderJob : IJob
{
    private readonly control_tareasContext dbContext;
    private readonly EmailSender emailSender;

    public TaskReminderJob(control_tareasContext dbContext, EmailSender emailSender)
    {
        this.dbContext = dbContext;
        this.emailSender = emailSender;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Ejecutando tarea de recordatorio...");
        // Obtiene la fecha de hoy como DateOnly
        var today = DateOnly.FromDateTime(DateTime.Today);

        Console.WriteLine("today" + today);

        // Lógica para consultar las tareas próximas a vencer y enviar correos electrónicos
        var tasks = dbContext.Tareas.Where(t => t.FechaLimite == today).ToList();

        Console.WriteLine("taks" + tasks.Count);

        foreach (var task in tasks)
        {
            Console.WriteLine("mira"+task.Id);
            var asignacionUsuario = await dbContext.AsignacionUsuarios.FirstOrDefaultAsync(au => au.TareaId == task.Id);
            var usuarioAsignado = asignacionUsuario != null ? await dbContext.Usuarios.FindAsync(asignacionUsuario.UsuarioId) : null;
            var correoUsuarioAsignado = usuarioAsignado?.Correo;
            // Aquí envías el correo electrónico de notificación para cada tarea
            await emailSender.SendTaskNotificationEmail(task, correoUsuarioAsignado);
        }
    }
}