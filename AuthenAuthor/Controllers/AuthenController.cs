using AuthenAuthor.Models.RequestModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthenAuthor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenController : ControllerBase
{
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("forbidden")]
    public HttpResponseMessage GetForbidden()
    {
        return new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> GetLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return Ok("Logout with cookie is success");
    }

    [HttpPost("login-cookie")]
    public async Task<IActionResult> LoginCookie([FromBody] UserRequestModel userRequestModel)
    {
        // check user
        // get role
        var roleName = string.Empty;

        if (userRequestModel.UserName.EndsWith("Admin"))
            roleName = "Admin";
        else
            roleName = "Guest";

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, userRequestModel.UserName),
            new(ClaimTypes.Role, roleName),
            new("Fullname", "Pham Duong Minh An"),
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

        await HttpContext
            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

        return Ok("Login with cookie is success");
    }
}

