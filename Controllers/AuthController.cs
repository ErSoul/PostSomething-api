using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PostSomething_api.Models;
using PostSomething_api.Requests;
using PostSomething_api.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace PostSomething_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserManager<ApiUser> users, IEmailSender emailSender, IConfiguration config) : ControllerBase
{
    private readonly IUserManager<ApiUser> _userManager = users;
    private readonly IEmailSender _emailSender = emailSender;
    private readonly IConfiguration _config = config;

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserBody user)
    {
        if (user.Password != user.ConfirmationPassword)
            return new BadRequestObjectResult(new[] { new { code = "PasswordsMismatch", description = "The password must match the one above" } });

        var newUser = new ApiUser { Email = user.Email, UserName = user.UserName };
        var result = await _userManager.CreateAsync(newUser, user.Password);

        if (!result.Succeeded)
            return new UnprocessableEntityObjectResult(result.Errors);

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

        Request.Host = new HostString(_config["AppURL"]!);

        var callbackUrl = Url.Link(
            "Confirmation",
             new { userId = newUser.Id, code }
        );

        if (callbackUrl != null)
            await _emailSender.SendEmailAsync(user.Email, "Confirmar Correo Electrónico",
                $"Por favor, confirma tu cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>haciendo click aquí</a>.");

        return new JsonResult(new { user.Email, user.UserName });
    }

    [Route("confirmation", Name = "Confirmation")]
    [HttpGet]
    public async Task<IActionResult> ConfirmAccount([FromQuery] string userId, [FromQuery] string code)
    {
        if (userId == null || code == null)
            return UnprocessableEntity();

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return NotFound();

        var result = await _userManager.ConfirmEmailAsync(user, code);
        return result.Succeeded ? Ok() : BadRequest(result.Errors);
    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is not null)
        {
            var checkPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkPassword && user.EmailConfirmed)
                return Ok(new { token = GenerateJSONWebToken(user) });
        }

        return new UnauthorizedObjectResult("Wrong credentials.");
    }

    [Route("logout")]
    [HttpPost]
    public Task<IActionResult> Logout()
    {
        throw new NotImplementedException();
    }

    [Route("profile")]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var userEmail = User.FindFirstValue(JwtRegisteredClaimNames.Email);
        return Ok(await _userManager.FindByEmailAsync(userEmail!));
    }

    private string GenerateJSONWebToken(ApiUser user)
    {
#pragma warning disable CS8604
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Sid, user.Id),
            new Claim(JwtRegisteredClaimNames.Name, user.UserName)
        };

        var token = new JwtSecurityToken(
          issuer: _config["Jwt:Issuer"],
          audience: _config["Jwt:Audience"],
          claims: claims,
          expires: DateTime.Now.AddMinutes(2),
          signingCredentials: credentials);
#pragma warning restore CS8604
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}