using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StocksAndShares.Client.ApiGateway.Factory
{
    public sealed class StocksAndSharesHttpClient : IStocksAndSharesHttpClient
    {
        private readonly HttpContext _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public StocksAndSharesHttpClient(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TResult> GetApiAsync<TResult>(string url)
        {
            var accessToken = await _httpContext.GetTokenAsync("access_token");
            var apiClient = _httpClientFactory.CreateClient();

            //set token to the request.
            apiClient.SetBearerToken(accessToken);

            var response = await apiClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return typeof(TResult) != typeof(string) ?
                await ConvertToModel<TResult>(response) :
                (TResult)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(TResult));
            }

            throw new UnauthorizedAccessException($"{response.StatusCode} : {response.ReasonPhrase}");

        }

        private async Task<TResult> ConvertToModel<TResult>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResult>(content);
            return response;
        }
    }
}
