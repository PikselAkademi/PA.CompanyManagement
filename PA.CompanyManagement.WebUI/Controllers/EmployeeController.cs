using Microsoft.AspNetCore.Mvc;
using PA.CompanyManagement.EmployeeService.Application.DTOs.Requests;
using PA.CompanyManagement.WebUI.Clients.Employee;
using System.Threading.Tasks;

namespace PA.CompanyManagement.WebUI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeApiClient _client;

        public EmployeeController(IEmployeeApiClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _client.GetAllAsync();
            return View(model);
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            var model = await _client.GetAsync(id);

            if (model is null)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreateRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await _client.CreateAsync(model);

                if (response is null)
                    return View(response);

                return RedirectToAction(nameof(Detail), new { id = response.Id });
            }

            return View(model);
        }

        public async Task<IActionResult> Update(Guid id)
        {
            var model = await _client.GetAsync(id);

            if (model is null)
                return RedirectToAction(nameof(Index));

            return View(new EmployeeUpdateRequest
            {
                Id = id,
                Address = model.Address,
                EmailAddress = model.EmailAddress,
                PhoneNumber = model.PhoneNumber,
                UpdatedBy = Guid.Empty
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeUpdateRequest model)
        {
            if (ModelState.IsValid)
            {
                await _client.UpdateAsync(model);
                return RedirectToAction(nameof(Detail), new { id = model.Id });
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _client.DeleteAsync(id);
                return Json(new { success = true, message = "Veri silindi" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
