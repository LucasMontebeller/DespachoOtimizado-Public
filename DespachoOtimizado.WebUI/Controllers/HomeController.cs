using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DespachoOtimizado.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using DespachoOtimizado.WebUI.Filters;
using DespachoOtimizado.Application;
using DespachoOtimizado.Infra.Data;

namespace DespachoOtimizado.WebUI.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IOptimizerService _optmizerService;

    public HomeController(ILogger<HomeController> logger, IOptimizerService optmizerService)
    {
        _logger = logger;
        _optmizerService = optmizerService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [TypeFilter(typeof(TokenValidationFilter))] 
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> Process(IFormCollection file)
    {
        var xlsBytes = await file.ToArrayBytes();
        var fileName = file.Files?.FirstOrDefault()?.FileName ?? string.Empty;
        
        // Validar limitação do tipo no próprio formulário usando js
        var contentType = file.Files?.FirstOrDefault()?.ContentType ?? string.Empty;

        var response = await _optmizerService.SendRequest(xlsBytes, fileName, contentType);
        if (response.Success is false)
            ModelState.AddModelError("PythonResponse", response.Message);

        // Implementar caso sucesso
        return View("Index");
    }
}
