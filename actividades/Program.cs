using actividades.Models;
using actividades.Services.auth;
using actividades.Services.usuarios;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<control_tareasContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PubContext"));
});


builder.Services.AddScoped<IUsuariosService,UsuariosService>();

builder.Services.AddScoped<IAuthService, AuthService>();

var app = builder.Build();


app.UseCors(c =>
    c.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    .WithExposedHeaders("location")
    .WithExposedHeaders("content-disposition")
);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
