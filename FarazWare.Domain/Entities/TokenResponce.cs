using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarazWare.Domain.Entities
{
    public class TokenResponse
    {
        public string? AccessToken { get; set; }  // The token used to access protected resources
        public string? TokenType { get; set; }    // The type of token (e.g., "bearer")
        public DateTime ExpiresAt { get; set; }        // The lifetime in seconds of the access token
        public string? RefreshToken { get; set; }  // Token used to obtain a new access token
        public string? Scope { get; set; }         // The scope(s) of access granted by the token
        public string? BankId { get; set; }        // Identifier for the bank
        public string? MobileNumber { get; set; }  // The mobile number of the user
        public string? Channel { get; set; }       // The channel through which the token was issued
        public DateTime CreationDate { get; set; }    // Timestamp of when the token was created
        public List<string>? Authorities { get; set; } // List of authorities granted by the token
        public List<string>? Deposits { get; set; } // List of deposits associated with the token
        public string? Jti { get; set; }           // JWT ID, a unique identifier for the token
        public string? CsrfToken { get; set; }     // CSRF token for protecting against CSRF attacks

    }
}
