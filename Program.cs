using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configurar CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()  // Permite solicitudes desde cualquier origen
                          .AllowAnyMethod()  // Permite cualquier método HTTP (GET, POST, etc.)
                          .AllowAnyHeader();  // Permite cualquier cabecera
                });
            });

            // Configurar DbContext con la cadena de conexión desde appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Agregar otros servicios como controladores
            builder.Services.AddControllers();
            builder.Services.AddScoped<ITareaService, TareaService>(); // Agregar el servicio TareaService

            // Agregar Swagger para la documentación de la API
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagerAPI", Version = "v1" });
            });

            var app = builder.Build();

            // Configurar el pipeline de la solicitud HTTP
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskManagerAPI v1");
                });
            }

            app.UseHttpsRedirection();

            // Usar CORS
            app.UseCors("AllowAllOrigins");

            // Rutas de la API
            app.MapControllers();

            app.Run();
        }
    }
}
