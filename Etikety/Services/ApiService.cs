using Etikety.Interfaces;
using Etikety.Models;
using Newtonsoft.Json;

namespace Etikety.Services;

public class ApiService:IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://192.168.100.201:7070/Production");
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<ApiLabelResponse> GetLabel(UserData data)
    {
        var res = new ApiLabelResponse();
        return res;
        var response = await _httpClient.PostAsync("LabelData",new StringContent(JsonConvert.SerializeObject(data)));
        if (!response.IsSuccessStatusCode)
        {
            var apiResponse = new ApiLabelResponse()
            {
                OrderId ="NoConnection" 
            };
            return apiResponse;
        }

        var apiLabelResponse = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ApiLabelResponse>(apiLabelResponse);
        
        return result;

    }
}