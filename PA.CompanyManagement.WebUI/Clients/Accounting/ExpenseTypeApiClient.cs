using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PA.CompanyManagement.AccountingService.Application.DTOs.Requests.Types;
using PA.CompanyManagement.AccountingService.Application.DTOs.Responses.Types;
using PA.CompanyManagement.AccountingService.Application.Repositories.Types;
using System.Net;
using System.Text;

namespace PA.CompanyManagement.WebUI.Clients.Accounting
{
    public interface IExpenseTypeApiClient : IExpenseTypeRepository
    {

    }

    public class ExpenseTypeApiClient : IExpenseTypeApiClient
    {
        private readonly HttpClient _client;

        public ExpenseTypeApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ExpenseTypeResponse> CreateAsync(ExpenseTypeCreateRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                StringContent content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("", content);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ExpenseTypeResponse>(data);
                return model!;
            }
            catch (Exception ex)
            {
                return new ExpenseTypeResponse();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var response = await _client.DeleteAsync(id.ToString());
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<ExpenseTypeResponse>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync("");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<ExpenseTypeResponse>>(content);
                return model!;
            }
            catch (Exception ex)
            {
                return new List<ExpenseTypeResponse>();
            }
        }

        public async Task<ExpenseTypeResponse?> GetAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync(id.ToString());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ExpenseTypeResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DetailedExpenseTypeResponse?> GetDetailedAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync($"detailed/{id.ToString()}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DetailedExpenseTypeResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task UpdateAsync(ExpenseTypeUpdateRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                var content = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(request.Id.ToString(), content);
                response.EnsureSuccessStatusCode();

                //if (response.StatusCode == HttpStatusCode.NotFound)
                //{

                //}
                //else if (response.StatusCode == HttpStatusCode.BadGateway)
                //{

                //}

                //switch (response.StatusCode)
                //{
                //    case HttpStatusCode.Continue:
                //        break;
                //    case HttpStatusCode.SwitchingProtocols:
                //        break;
                //    case HttpStatusCode.Processing:
                //        break;
                //    case HttpStatusCode.EarlyHints:
                //        break;
                //    case HttpStatusCode.OK:
                //        break;
                //    case HttpStatusCode.Created:
                //        break;
                //    case HttpStatusCode.Accepted:
                //        break;
                //    case HttpStatusCode.NonAuthoritativeInformation:
                //        break;
                //    case HttpStatusCode.NoContent:
                //        break;
                //    case HttpStatusCode.ResetContent:
                //        break;
                //    case HttpStatusCode.PartialContent:
                //        break;
                //    case HttpStatusCode.MultiStatus:
                //        break;
                //    case HttpStatusCode.AlreadyReported:
                //        break;
                //    case HttpStatusCode.IMUsed:
                //        break;
                //    case HttpStatusCode.Ambiguous:
                //        break;
                //    case HttpStatusCode.Moved:
                //        break;
                //    case HttpStatusCode.Found:
                //        break;
                //    case HttpStatusCode.RedirectMethod:
                //        break;
                //    case HttpStatusCode.NotModified:
                //        break;
                //    case HttpStatusCode.UseProxy:
                //        break;
                //    case HttpStatusCode.Unused:
                //        break;
                //    case HttpStatusCode.RedirectKeepVerb:
                //        break;
                //    case HttpStatusCode.PermanentRedirect:
                //        break;
                //    case HttpStatusCode.BadRequest:
                //        break;
                //    case HttpStatusCode.Unauthorized:
                //        break;
                //    case HttpStatusCode.PaymentRequired:
                //        break;
                //    case HttpStatusCode.Forbidden:
                //        break;
                //    case HttpStatusCode.NotFound:
                //        break;
                //    case HttpStatusCode.MethodNotAllowed:
                //        break;
                //    case HttpStatusCode.NotAcceptable:
                //        break;
                //    case HttpStatusCode.ProxyAuthenticationRequired:
                //        break;
                //    case HttpStatusCode.RequestTimeout:
                //        break;
                //    case HttpStatusCode.Conflict:
                //        break;
                //    case HttpStatusCode.Gone:
                //        break;
                //    case HttpStatusCode.LengthRequired:
                //        break;
                //    case HttpStatusCode.PreconditionFailed:
                //        break;
                //    case HttpStatusCode.RequestEntityTooLarge:
                //        break;
                //    case HttpStatusCode.RequestUriTooLong:
                //        break;
                //    case HttpStatusCode.UnsupportedMediaType:
                //        break;
                //    case HttpStatusCode.RequestedRangeNotSatisfiable:
                //        break;
                //    case HttpStatusCode.ExpectationFailed:
                //        break;
                //    case HttpStatusCode.MisdirectedRequest:
                //        break;
                //    case HttpStatusCode.UnprocessableEntity:
                //        break;
                //    case HttpStatusCode.Locked:
                //        break;
                //    case HttpStatusCode.FailedDependency:
                //        break;
                //    case HttpStatusCode.UpgradeRequired:
                //        break;
                //    case HttpStatusCode.PreconditionRequired:
                //        break;
                //    case HttpStatusCode.TooManyRequests:
                //        break;
                //    case HttpStatusCode.RequestHeaderFieldsTooLarge:
                //        break;
                //    case HttpStatusCode.UnavailableForLegalReasons:
                //        break;
                //    case HttpStatusCode.InternalServerError:
                //        break;
                //    case HttpStatusCode.NotImplemented:
                //        break;
                //    case HttpStatusCode.BadGateway:
                //        break;
                //    case HttpStatusCode.ServiceUnavailable:
                //        break;
                //    case HttpStatusCode.GatewayTimeout:
                //        break;
                //    case HttpStatusCode.HttpVersionNotSupported:
                //        break;
                //    case HttpStatusCode.VariantAlsoNegotiates:
                //        break;
                //    case HttpStatusCode.InsufficientStorage:
                //        break;
                //    case HttpStatusCode.LoopDetected:
                //        break;
                //    case HttpStatusCode.NotExtended:
                //        break;
                //    case HttpStatusCode.NetworkAuthenticationRequired:
                //        break;
                //    default:
                //        break;
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}
