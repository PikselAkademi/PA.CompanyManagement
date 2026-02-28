using PA.CompanyManagement.WebUI.Clients.Accounting;
using PA.CompanyManagement.WebUI.Clients.Employee;
using System.Net.Http.Headers;

namespace PA.CompanyManagement.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<IEmployeeApiClient, EmployeeApiClient>(s =>
            {
                s.BaseAddress = new Uri("https://localhost:7255/api/emp/");

                s.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddHttpClient<IIncomeTypeApiClient, IncomeTypeApiClient>(s =>
            {
                s.BaseAddress = new Uri("https://localhost:7255/api/ty/ic/");

                s.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddHttpClient<IExpenseTypeApiClient, ExpenseTypeApiClient>(s =>
            {
                s.BaseAddress = new Uri("https://localhost:7255/api/ty/ex/");

                s.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddHttpClient<IIncomeApiClient, IncomeApiClient>(s =>
            {
                s.BaseAddress = new Uri("https://localhost:7255/api/mt/ic/");

                s.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            builder.Services.AddHttpClient<IExpenseApiClient, ExpenseApiClient>(s =>
            {
                s.BaseAddress = new Uri("https://localhost:7255/api/mt/ex/");

                s.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
