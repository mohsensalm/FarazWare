using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FarazWare.Domain.Dto
{
    public record CardDto
    {
        [JsonPropertyName("cardStatus")]
        public string? CardStatus { get; set; }
        [JsonPropertyName("cardType")]
        public string? CardType { get; set; }
        [JsonPropertyName("cardTypeResponse")]
        public string? CardTypeResponse { get; set; }
        [JsonPropertyName("customerFirstName")]
        public string? CustomerFirstName { get; set; }
        [JsonPropertyName("customerLastName")]
        public string? CustomerLastName { get; set; }
        [JsonPropertyName("depositNumber")]
        public string? DepositNumber { get; set; }
        [JsonPropertyName("expireDate")]
        public DateTimeOffset ExpireDate { get; set; }
        [JsonPropertyName("issueDate")]
        public DateTimeOffset IssueDate { get; set; }
        [JsonPropertyName("pan")]
        public string? Pan { get; set; }
    }
}
