using eShopFlix.Web.HttpClients;
using eShopFlix.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShopFlix.Web.Controllers;

public class AccountController : Controller
{
    private readonly AuthServiceClient _authServiceClient;

    public AccountController(AuthServiceClient authServiceClient)
    {
        _authServiceClient = authServiceClient;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginModel loginModel, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            var user =  _authServiceClient.LoginAsync(loginModel).Result;
            if (user != null)
            {
                GenerateTicket(user);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else if (user.Roles.Contains("User"))
                {
                    return RedirectToAction("Index","Home", new {area = "User"});
                }
            }

            ModelState.AddModelError("","Invalid login attempt.");
        }

        return View(loginModel);
    }

    private void GenerateTicket(UserModel user)
    {
        var strData = JsonSerializer.Serialize(user);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.UserData, strData),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, String.Join(',', user.Roles))
        };

        var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties()
        {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            IsPersistent = true
        };

        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                new ClaimsPrincipal(claimIdentity),
                                authProperties);
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).Wait();
        return RedirectToAction("Login");
    }

    public IActionResult UnAUthorize()
    {
        return View();
    }

}
