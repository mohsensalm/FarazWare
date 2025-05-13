using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarazWare.Domain.Entities;

namespace FarazWare.Infrastructure.Clients
{
    public interface IClientCredentialsService
    {
        Task<TokenResponse> GetTokenAsync(string mobileNumber, string state);

    }
}
