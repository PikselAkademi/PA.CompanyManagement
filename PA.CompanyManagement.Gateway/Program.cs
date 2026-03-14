using Grafana.OpenTelemetry;

namespace PA.CompanyManagement.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

            builder.Services.AddOpenTelemetry()
                .UseGrafana();

            builder.Logging.AddOpenTelemetry(conf =>
            {
                conf.UseGrafana();
            });

            var app = builder.Build();

            app.MapReverseProxy();

            app.Run();
        }
    }
}
