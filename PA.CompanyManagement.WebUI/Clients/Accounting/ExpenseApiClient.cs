using Newtonsoft.Json;
using PA.CompanyManagement.AccountingService.Application.DTOs.Requests.Metas;
using PA.CompanyManagement.AccountingService.Application.DTOs.Responses.Metas;
using PA.CompanyManagement.AccountingService.Application.Repositories.Metas;
using System.Text;

namespace PA.CompanyManagement.WebUI.Clients.Accounting
{
    public interface IExpenseApiClient : IExpenseRepository
    {

    }


    public class ExpenseApiClient : IExpenseApiClient
    {
        private readonly HttpClient _client;

        public ExpenseApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<ExpenseResponse> CreateAsync(ExpenseCreateRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                var stc = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("", stc);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ExpenseResponse>(content);
                return model!;
            }
            catch (Exception)
            {
                return new ExpenseResponse();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                var response = await _client.DeleteAsync(id.ToString());
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }

        public async Task<List<MinimalExpenseResponse>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync("");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<MinimalExpenseResponse>>(content);
                return model!;
            }
            catch (Exception)
            {
                return new List<MinimalExpenseResponse>();
            }
        }

        public async Task<List<MinimalExpenseResponse>> GetAllAsync(Guid incomeTypeId)
        {
            try
            {
                var response = await _client.GetAsync($"type/{incomeTypeId.ToString()}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<MinimalExpenseResponse>>(content);
                return model!;
            }
            catch (Exception)
            {
                return new List<MinimalExpenseResponse>();
            }
        }

        public async Task<ExpenseResponse?> GetAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync(id.ToString());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<ExpenseResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DetailedExpenseResponse?> GetDetailedAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync($"detailed/{id.ToString()}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DetailedExpenseResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task PatchAsync(ExpensePatchRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                var stc = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PatchAsync(request.Id.ToString(), stc);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }

        public async Task UpdateAsync(ExpenseUpdateRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                var stc = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PutAsync(request.Id.ToString(), stc);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {

            }
        }
    }
}
