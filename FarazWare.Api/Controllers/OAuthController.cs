using FarazWare.Infrastructure.Clients;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class OAuthController : ControllerBase
{
    private readonly IClientCredentialsService _clientCred;
    //private readonly IAuthorizationCodeService _authCode;
    //private readonly IRevokeService _revoke;
    //private readonly IRefreshService _refresh;
    //private readonly ILoginService _login;

    public OAuthController(
        IClientCredentialsService clientCred
        //IAuthorizationCodeService authCode,
        //IRevokeService revoke,
        //IRefreshService refresh,
        //ILoginService login)
        )
    {
        _clientCred = clientCred;
        //_authCode = authCode;
        //_revoke = revoke;
        //_refresh = refresh;
        //_login = login;
    }

    [HttpPost("token/client_credentials")]
    public async Task<IActionResult> ClientCredentials([FromQuery] string mobileNumber, [FromQuery] string state)
    {
        var token = await _clientCred.GetTokenAsync(mobileNumber, state);
        return Ok(token);
    }

    //[HttpGet("authorize")]
    //public IActionResult Authorize([FromQuery] AuthParams p)
    //{
    //    var url = _authCode.BuildAuthorizeUrl(p);
    //    return Redirect(url);
    //}

    //[HttpPost("token/authorization_code")]
    //public async Task<IActionResult> ExchangeCode([FromQuery] CodeExchangeParams p)
    //{
    //    var token = await _authCode.ExchangeCodeAsync(p);
    //    return Ok(token);
    //}

    //[HttpGet("revoke")]
    //public IActionResult RevokeRedirect([FromQuery] RevokeParams p)
    //{
    //    var url = _revoke.BuildRevokeUrl(p);
    //    return Redirect(url);
    //}

    //[HttpPost("revoke-token")]
    //public async Task<IActionResult> Revoke([FromBody] RevokeTokenDto dto)
    //{
    //    var result = await _revoke.RevokeTokenAsync(dto.Token);
    //    return Ok(result);
    //}

    //[HttpPost("token/refresh_token")]
    //public async Task<IActionResult> Refresh([FromQuery] string mobileNumber, [FromQuery] string refresh_token)
    //{
    //    var token = await _refresh.RefreshTokenAsync(mobileNumber, refresh_token);
    //    return Ok(token);
    //}

    //[HttpGet("login")]
    //public IActionResult Login([FromQuery] string state)
    //{
    //    var url = _login.BuildLoginUrl(state);
    //    return Redirect(url);
    //}
}