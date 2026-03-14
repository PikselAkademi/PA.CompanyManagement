
using Grafana.OpenTelemetry;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using PA.CompanyManagement.Core.Extensions;
using PA.CompanyManagement.EmployeeService.Infrastructure;

namespace PA.CompanyManagement.EmployeeService.Api.Rest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddOpenTelemetry()
                 .WithTracing(conf =>
                 {
                     //conf.AddAspNetCoreInstrumentation();

                     conf.UseGrafana().AddConsoleExporter();
                 })
                .WithMetrics(conf =>
                {
                    //conf.AddAspNetCoreInstrumentation();
                    //conf.AddRuntimeInstrumentation();
                    //conf.AddProcessInstrumentation();

                    conf.UseGrafana().AddConsoleExporter();
                });

            //builder.Services.AddOpenTelemetry()
            //    .UseGrafana(conf =>
            //    {
            //        conf.Instrumentations.Add(Instrumentation.AspNetCore);
            //        conf.Instrumentations.Add(Instrumentation.NetRuntime);
            //        conf.Instrumentations.Add(Instrumentation.Process);
            //    });

            //builder.Services.AddOpenTelemetry()
            //    .UseGrafana();

            builder.Logging.AddOpenTelemetry(conf =>
            {
                conf.UseGrafana().AddConsoleExporter();
            });

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        

            builder.Services.AddEmployeeContext(builder.Configuration);

            builder.Services.AddPASwagger(options =>
            {
                options.Title = "Employee API";
            });

            var app = builder.Build();

            var swopt = new PASwaggerOptions();
            app.UsePASwagger(swopt);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
