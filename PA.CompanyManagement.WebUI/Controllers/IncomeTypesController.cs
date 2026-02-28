using Microsoft.AspNetCore.Mvc;
using PA.CompanyManagement.WebUI.Clients.Accounting;
using System.Threading.Tasks;

namespace PA.CompanyManagement.WebUI.Controllers
{
    public class IncomeTypesController : Controller
    {
        private readonly IIncomeTypeApiClient _client;

        public IncomeTypesController(IIncomeTypeApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAllAsync();
            return View();
        }
    }
}
