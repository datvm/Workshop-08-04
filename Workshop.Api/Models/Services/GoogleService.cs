using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ServiceSharp;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Api.Models.Entities;
using Workshop.Api.Models.ViewModels;

namespace Workshop.Api.Models.Services
{

    public interface IGoogleService : IService
    {

        Uri GetOAuth2Url(string state);
        Task<GoogleUserInfoViewModel> GetUserFromCode(string code);

    }

    public class GoogleService : IGoogleService, IService<IGoogleService>
    {

        private static readonly RestClient RestClient = new RestClient("https://www.google.com");

        ApiSettings apiSettings;
        
        public GoogleService(ApiSettings apiSettings)
        {
            this.apiSettings = apiSettings;
        }

        public Uri GetOAuth2Url(string state)
        {
            var request = new RestRequest("https://accounts.google.com/o/oauth2/v2/auth")
                .AddQueryParameter("client_id", this.apiSettings.GoogleOAuth2.ClientId)
                .AddQueryParameter("redirect_uri", this.apiSettings.GoogleOAuth2.RedirectUrl)
                .AddQueryParameter("scope", "profile email")
                .AddQueryParameter("state", state)
                .AddQueryParameter("response_type", "code");

            return RestClient.BuildUri(request);
        }

        public async Task<GoogleUserInfoViewModel> GetUserFromCode(string code)
        {
            var tokenRequest = new RestRequest("https://www.googleapis.com/oauth2/v4/token", Method.POST)
                .AddQueryParameter("code", code)
                .AddQueryParameter("client_id", this.apiSettings.GoogleOAuth2.ClientId)
                .AddQueryParameter("client_secret", this.apiSettings.GoogleOAuth2.ClientSecret)
                .AddQueryParameter("redirect_uri", this.apiSettings.GoogleOAuth2.RedirectUrl)
                .AddQueryParameter("grant_type", "authorization_code");

            var tokenResponse = await RestClient.ExecuteTaskAsync(tokenRequest);
            if (!tokenResponse.IsSuccessful)
            {
                throw new Exception("Do something...");
            }

            var tokenResponseJson = JsonConvert.DeserializeObject<JObject>(tokenResponse.Content);
            var accessToken = tokenResponseJson["access_token"].Value<string>();

            var userInfoRequest = new RestRequest("https://www.googleapis.com/oauth2/v3/userinfo")
                .AddHeader("Authorization", "Bearer " + accessToken);

            var userInfoResponse = await RestClient.ExecuteTaskAsync(userInfoRequest);
            // Validate response here...

            return JsonConvert.DeserializeObject<GoogleUserInfoViewModel>(userInfoResponse.Content);
        }

    }

}
