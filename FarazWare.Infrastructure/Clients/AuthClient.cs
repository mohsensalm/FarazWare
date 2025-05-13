using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Application.Contracts.Services;
using Microsoft.Extensions.Configuration;

namespace FarazWare.Infrastructure.Clients
{
    public class AuthClient : IAuthClient
    {
        private readonly HttpClient _http;
        private readonly string _authAddress;
        private readonly string _basicAuth;

        public AuthClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _authAddress = config["AuthSettings:AuthAddress"];
            var key = config["AuthSettings:AppKey"];
            var secret = config["AuthSettings:AppSecret"];
            _basicAuth = "Basic " + Convert.ToBase64String(
                                 Encoding.UTF8.GetBytes($"{key}:{secret}"));
        }

        public async Task<string> GetTokenAsync(string encryptedCredentials, CancellationToken ct = default)
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _http.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse(_basicAuth);

            var payload = new
            {
                acceptorCode = "6901000000",
                clientAddress = "127.0.0.1",
                encryptedCredentials
            };
            using var resp = await _http.PostAsJsonAsync(
                $"{_authAddress}/login/v1/mobileLogin", payload, ct);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadAsStringAsync(ct);
        }
    }
}
