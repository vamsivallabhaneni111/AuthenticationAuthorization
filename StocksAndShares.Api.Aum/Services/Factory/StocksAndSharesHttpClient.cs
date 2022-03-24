using IdentityModel.Client;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StocksAndShares.Api.Aum.Services.Factory
{
    public sealed class StocksAndSharesHttpClient : IStocksAndSharesHttpClient
    {
        private const string base_url = "https://localhost:44389/api"; //move this to appsettings.json
        private readonly IHttpClientFactory _httpClientFactory;

        public StocksAndSharesHttpClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResult> GetApiAsync<TResult>(string route_url)
        {
            var apiClient = _httpClientFactory.CreateClient();

            //set token to the request.
            var tokenResponse = await GetTokenAsync();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync(base_url + route_url);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException($"{response.StatusCode} : {response.ReasonPhrase}");
            }

            return typeof(TResult) != typeof(string) ?
                await ConvertToModel<TResult>(response) :
                (TResult)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(TResult));
        }

        private async Task<TokenResponse> GetTokenAsync()
        {
            var serverClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await serverClient.GetDiscoveryDocumentAsync("https://localhost:44322/");

            return await serverClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest()
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "client_id",
                    ClientSecret = "client_secret",
                    Scope = "LiquidFunds"
                });
        }

        private async Task<TResult> ConvertToModel<TResult>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResult>(content);
            return response;
        }
    }
}
