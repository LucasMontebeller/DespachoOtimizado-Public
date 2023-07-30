using DespachoOtimizado.Domain.Interfaces.Account;
using DespachoOtimizado.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

public class AccountController : Controller
{
    private readonly ILogger<AccountController> _logger;
    private readonly IAuthenticateService _authenticate;

    public AccountController(ILogger<AccountController> logger, IAuthenticateService authenticate)
    {
        _logger = logger;
        _authenticate = authenticate;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl)
    {
        return View(new LoginViewModel()
        {
            ReturnUrl = returnUrl
        });
        
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
    {
        var result = await _authenticate.Authenticate(model.Email, model.Password);

        if (result)
        {
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");
            
            return RedirectToAction(model.ReturnUrl);
        }

        ModelState.AddModelError("Email", "Invalid email or password");
        return View(model);
    }

    [HttpGet]
    public IActionResult Register(string? returnUrl)
    {
        return View(new RegisterViewModel()
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl)
    {
        var existsUser = await _authenticate.ExistsUser(model.Email);
        if (existsUser)
        {
            ModelState.AddModelError("Email", "Já existe um usuário com este email");
            return View(model);
        }

        var (result, errors) = await _authenticate.RegisterUser(model.Name, model.Email, model.Phone, model.Password);
        if (!result)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("Create", error);
            }
            return View(model);
        }
        
        TempData["RegistrationSuccess"] = true;
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _authenticate.Logout();
        return RedirectToAction("Login", "Account");
    }
}
