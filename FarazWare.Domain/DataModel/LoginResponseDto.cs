using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarazWare.Domain.Dto
{
    public record LoginResponseDto
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }
        [JsonPropertyName("referenceNumber")]
        public string? ReferenceNumber { get; set; }
        [JsonPropertyName("transactionDate")]
        public string? TransactionDate { get; set; }
        [JsonPropertyName("token")]
        public string? Token { get; set; }

    }
}
