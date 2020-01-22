using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient
{
    public class Client
    {
        private HttpClient _client;
        private string _bearerToken;

        public async Task<string> Authorize()
        {
            _client = new HttpClient();
            HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, @"https://localhost:44371/OAuth2/Authorize");

            var json = JsonConvert.SerializeObject(new { Name = "mciec", Password = "password" });
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            req.Content = data;
            var resp = await _client.SendAsync(req).ConfigureAwait(false);
            _bearerToken = await resp.Content.ReadAsStringAsync().ConfigureAwait(false);
            return _bearerToken;
        }

        public async Task<string> GetResource()
        {
            _client = new HttpClient();
            HttpRequestMessage req1 = new HttpRequestMessage(HttpMethod.Get, @"https://localhost:44371/Api/Get1");
            req1.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            var resp1 = await _client.SendAsync(req1);
            var respContent1 = await resp1.Content.ReadAsStringAsync();

            HttpRequestMessage req2 = new HttpRequestMessage(HttpMethod.Get, @"https://localhost:44371/Api/Get2");
            req2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _bearerToken);
            var resp2 = await _client.SendAsync(req2);
            var respContent2 = await resp2.Content.ReadAsStringAsync();

            return respContent2;

        }


    }
}
