using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PA.CompanyManagement.EmployeeService.Application.DTOs.Requests;
using PA.CompanyManagement.EmployeeService.Application.Repositories;

namespace PA.CompanyManagement.EmployeeService.Api.Rest.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeRepository repository, ILogger<EmployeesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _logger.LogInformation("GetAll Methodu tetiklendi.");
            try
            {
                var response = await _repository.GetAllAsync();

                if (response.Count > 0)
                    return Ok(response);

                _logger.LogInformation("Çalışan bulunamadı");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} -  Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            _logger.LogInformation($"Çalışan Çağırıldı. (ID:{id})");
            try
            {
                var response = await _repository.GetAsync(id);

                if (response is null)
                {
                    _logger.LogWarning($"Çalışan bulunamdı. (ID:{id})");
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} -  Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }

        [HttpGet("detailed/{id:guid}")]
        public async Task<IActionResult> GetDetailedAsync(Guid id)
        {
            _logger.LogInformation($"Detaylı Çalışan Çağırıldı. (ID:{id})");
            try
            {
                var response = await _repository.GetDetailedAsync(id);

                if (response is null)
                {
                    _logger.LogInformation($"Çalışan Bulunamadı. (ID:{id})");
                    return NotFound();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} -  Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(EmployeeCreateRequest request)
        {
            _logger.LogInformation($"Yeni kullanıcı ekleme isteğinde bulunuldu. (Email:{request.EmailAddress})");
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("HTTP 400 | Model Geçersiz.");
                    return Problem(
                        statusCode: StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"HTTP 400 | Model Geçersiz. (Email:{request.EmailAddress})");
                    return ValidationProblem(
                        statusCode: StatusCodes.Status400BadRequest,
                        modelStateDictionary: ModelState);
                }

                var response = await _repository.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetAllAsync),
                    new { id = response.Id },
                    response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} - Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync(Guid id, EmployeeUpdateRequest request)
        {
            _logger.LogInformation($"Kullanıcı düzenleme isteğinde bulunuldu. (Email:{request.EmailAddress})");
            try
            {
                if (request is null)
                {
                    _logger.LogWarning("HTTP 400 | Model Geçersiz.");
                    return Problem(
                        statusCode: StatusCodes.Status400BadRequest);
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogWarning($"HTTP 400 | Model Geçersiz. (Email:{request.EmailAddress})");
                    return ValidationProblem(
                        statusCode: StatusCodes.Status400BadRequest,
                        modelStateDictionary: ModelState);
                }

                await _repository.UpdateAsync(request);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} -  Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            _logger.LogInformation($"Kullanıcı silme isteğinde bulunuldu. (ID:{id})");
            try
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{ex.Message} | Path:{Request.Path} - Method:{Request.Method} -  Query:{Request.Query}");
                return Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    title: ex.Message);
            }
        }
    }
}
