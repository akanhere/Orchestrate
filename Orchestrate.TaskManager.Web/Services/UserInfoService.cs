using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Orchestrate.TaskManager.Web.Models;

namespace Orchestrate.TaskManager.Web.Services
{
    public class UserInfoService : IUserInfoService
    {
        private HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IOptions<AppSettings> _settings;

        public UserInfoService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _remoteServiceBaseUrl = $"{settings.Value.UserInfoApiUri}/api/v1/AppUsers";
        }


        public async Task<List<UserInfo>> GetAllUsers()
        {
            var uri = _remoteServiceBaseUrl;
            var responseString = await _httpClient.GetStringAsync(uri);
            var response = JsonConvert.DeserializeObject<List<UserInfo>>(responseString);
            return response;
        }


    }
}
