using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarazWare.Infrastructure.Clients
{
    public interface IAuthClient
    {
        Task<string> GetTokenAsync(string encryptedCredentials, CancellationToken ct = default);

    }
}
