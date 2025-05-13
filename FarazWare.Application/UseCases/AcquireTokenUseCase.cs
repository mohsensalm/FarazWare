using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Application.Contracts.Services;
using FarazWare.Domain.Dto;
using FarazWare.Infrastructure.Services;
using Newtonsoft.Json;

namespace FarazWare.Application.UseCases
{
    public class AcquireTokenUseCase
    {
        private readonly IEncryptionService _encryption;
        private readonly IAuthClient _auth;

        public AcquireTokenUseCase(IEncryptionService encryption, IAuthClient auth)
        {
            _encryption = encryption;
            _auth = auth;
        }

        public async Task<string> ExecuteAsync(string username, string password)
        {
            var creds = $"{username}|{password}|";
            var encrypted = _encryption.Encrypt(creds);
            var raw = await _auth.GetTokenAsync(encrypted);
            var dto = JsonConvert.DeserializeObject<LoginResponseDto>(raw);
            return dto.Token;
        }
    }
}