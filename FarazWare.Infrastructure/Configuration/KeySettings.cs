using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarazWare.Infrastructure.Configuration
{
    public class KeySettings
    {
        public string PublicKeyPemPath { get; set; } = default!;
        public string? PrivateKeyPemPath { get; set; }
    }
}
