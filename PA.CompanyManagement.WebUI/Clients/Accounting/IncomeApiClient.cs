using Newtonsoft.Json;
using PA.CompanyManagement.AccountingService.Application.DTOs.Requests.Metas;
using PA.CompanyManagement.AccountingService.Application.DTOs.Responses.Metas;
using PA.CompanyManagement.AccountingService.Application.Repositories.Metas;
using System.Text;

namespace PA.CompanyManagement.WebUI.Clients.Accounting
{
    public interface IIncomeApiClient : IIncomeRepository
    {

    }


    public class IncomeApiClient : IIncomeApiClient
    {
        private readonly HttpClient _client;

        public IncomeApiClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<IncomeResponse> CreateAsync(IncomeCreateRequest request)
        {
            try
            {
                var serialized = JsonConvert.SerializeObject(request);
                var stc = new StringContent(serialized, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("", stc);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IncomeResponse>(content);
                return model!;
            }
            catch (Exception)
            {
                return new IncomeResponse();
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

        public async Task<List<MinimalIncomeResponse>> GetAllAsync()
        {
            try
            {
                var response = await _client.GetAsync("");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<MinimalIncomeResponse>>(content);
                return model!;
            }
            catch (Exception)
            {
                return new List<MinimalIncomeResponse>();
            }
        }

        public async Task<List<MinimalIncomeResponse>> GetAllAsync(Guid incomeTypeId)
        {
            try
            {
                var response = await _client.GetAsync($"type/{incomeTypeId.ToString()}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<List<MinimalIncomeResponse>>(content);
                return model!;
            }
            catch (Exception)
            {
                return new List<MinimalIncomeResponse>();
            }
        }

        public async Task<IncomeResponse?> GetAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync(id.ToString());
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<IncomeResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<DetailedIncomeResponse?> GetDetailedAsync(Guid id)
        {
            try
            {
                var response = await _client.GetAsync($"detailed/{id.ToString()}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<DetailedIncomeResponse>(content);
                return model;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task PatchAsync(IncomePatchRequest request)
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

        public async Task UpdateAsync(IncomeUpdateRequest request)
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
