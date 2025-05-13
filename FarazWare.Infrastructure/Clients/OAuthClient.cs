using FarazWare.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

public class OAuthClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _config;

    public OAuthClient(HttpClient http, IConfiguration config)
    {
        _http = http;
        _config = config;
    }

    public async Task<TokenResponse> RequestClientCredentialsAsync(
        string mobileNumber,
        int bankId,
        string state="true")
    {
        var auth = Convert.ToBase64String(
            Encoding.UTF8.GetBytes($"{_config["AuthSettings:AppKey"]}:{_config["AuthSettings:AppSecret"]}"));

        var req = new HttpRequestMessage(HttpMethod.Post,
            $"{_config["AuthSettings:AuthAddress"]}/oauth/token?grant_type=client_credentials&mobileNumber={mobileNumber}&bankId={bankId}&state={state}");
        req.Headers.Authorization = new AuthenticationHeaderValue("Basic", auth);
        req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var res = await _http.SendAsync(req);
        res.EnsureSuccessStatusCode();
        var payload = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TokenResponse>(payload);
    }

}