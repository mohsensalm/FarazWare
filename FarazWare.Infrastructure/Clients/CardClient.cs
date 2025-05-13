using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Application.Contracts.Services;
using FarazWare.Domain.Dto;
using Microsoft.Extensions.Configuration;

namespace FarazWare.Infrastructure.Clients
{
    public class CardClient : ICardClient
    {
        private readonly HttpClient _http;
        private readonly string _baseAddress;

        public CardClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _baseAddress = config["AuthSettings:AuthAddress"];
        }

        public async Task<CardListDto> GetCardsAsync(string token, CancellationToken ct = default)
        {
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var payload = new
            {
                acceptorCode = "6901000000",
                clientAddress = "127.0.0.1",
                channel = "WEB",
                authorizedUserInfo = token,
                cardStatus = "OK",
                length = 10,
                offset = 0
            };
            using var resp = await _http.PostAsJsonAsync(
                $"{_baseAddress}/private/card/v1/getCards", payload, ct);
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<CardListDto>(ct);
        }
    }
}