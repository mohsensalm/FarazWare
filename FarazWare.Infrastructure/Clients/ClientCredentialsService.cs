using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetOpenAuth.AspNet.Clients;
using FarazWare.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace FarazWare.Application.Contracts.Services
{
    public class ClientCredentialsService : IClientCredentialsService
    {
        private readonly OAuthClient _client;
        private readonly IConfiguration _config;

        public ClientCredentialsService(OAuthClient client, IConfiguration config)
        {
            _client = client;
            _config = config;
        }

        public async Task<TokenResponse> GetTokenAsync(string mobileNumber, string state)
        {
            var bankIdStr = _config["AuthSettings:BankId"];            // خواندن به‌صورت string
            int bankId = int.TryParse(bankIdStr, out var tmp) ? tmp : 69;   //69 is iranzamin bankid
            return await _client.RequestClientCredentialsAsync(
                mobileNumber,
               bankId,
                state);
        }
    }
}
