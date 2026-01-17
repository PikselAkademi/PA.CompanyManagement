using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PA.CompanyManagement.AccountingService.Application.Repositories.Types;
using System.Threading.Tasks;

namespace PA.CompanyManagement.AccountingService.Api.Rest.Controllers.Types
{
    [Route("api/expense-type")]
    [ApiController]
    public class ExpenseTypesController : ControllerBase
    {
        private readonly IExpenseTypeRepository _repository;

        public ExpenseTypesController(IExpenseTypeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(await _repository.GetAllAsync());
        }
    }
}
