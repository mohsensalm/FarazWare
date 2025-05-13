using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarazWare.Domain.Dto
{
    public record CardListDto
    {
        [JsonPropertyName("status")]
        public long Status { get; set; }
        [JsonPropertyName("referenceNumber")]
        public Guid ReferenceNumber { get; set; }
        [JsonPropertyName("transactionDate")]
        public DateTimeOffset TransactionDate { get; set; }
        [JsonPropertyName("cards")]
        public List<CardDto>? Cards { get; set; }
    }
}
