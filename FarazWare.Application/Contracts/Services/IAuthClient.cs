using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarazWare.Application.Contracts.Services
{
    public interface IAuthClient
    {
        Task<string> GetTokenAsync(string encryptedCredentials, CancellationToken ct = default);

    }
}
