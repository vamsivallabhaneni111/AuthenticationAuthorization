using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StocksAndShares.Client
{
    public class StockAndSharesClientManager : IStockAndSharesClientManager
    {
        private const string BaseURL_AUM = "https://localhost:44368/api";
        private readonly HttpContext _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public StockAndSharesClientManager(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TResult> GetResourceAsync<TResult>(string url)
        {
            var token = await _httpContext.GetTokenAsync("access_token");
            var client = _httpClientFactory.CreateClient();
            client.SetBearerToken(token);
            var response = await client.GetAsync(BaseURL_AUM + url);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException($"{response.StatusCode} : {response.ReasonPhrase}");
            }

            return typeof(TResult) != typeof(string) ?
                await ConvertToModel<TResult>(response) :
                (TResult)Convert.ChangeType(await response.Content.ReadAsStringAsync(), typeof(TResult));
        }

        public async Task<HttpResponseMessage> AccessTokenRefreshWrapper(Func<Task<HttpResponseMessage>> securedRequest)
        {
            var response = await securedRequest();
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await RefreshAccessToken();
                response = await securedRequest();
            }
            return response;
        }

        private async Task RefreshAccessToken()
        {
            var identityClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await identityClient.GetDiscoveryDocumentAsync("https://localhost:44322/");

            var refreshToken = await _httpContext.GetTokenAsync("refresh_token");

            var refreshTokenClient = _httpClientFactory.CreateClient();

            var tokenResponse = await refreshTokenClient.RequestRefreshTokenAsync(
                new RefreshTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    RefreshToken = refreshToken,
                    ClientId = "client_id_mvc",
                    ClientSecret = "client_secret_mvc"
                });


            var authInfo = await _httpContext.AuthenticateAsync("Cookie");

            authInfo.Properties.UpdateTokenValue("access_token", tokenResponse.AccessToken);
            authInfo.Properties.UpdateTokenValue("refresh_token", tokenResponse.RefreshToken);

            await _httpContext.SignInAsync("Cookie", authInfo.Principal, authInfo.Properties);

            return;
        }

        private async Task<TResult> ConvertToModel<TResult>(HttpResponseMessage responseMessage)
        {
            var content = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<TResult>(content);
            return response;
        }
    }
}
