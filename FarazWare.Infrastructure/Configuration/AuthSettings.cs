using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarazWare.Infrastructure.Configuration
{
    public class AuthSettings
    {
        public string AuthAddress { get; set; } = default!;
        public string AppKey { get; set; } = default!;
        public string AppSecret { get; set; } = default!;
        public string BankId { get; set; } = "69";
    }
}
