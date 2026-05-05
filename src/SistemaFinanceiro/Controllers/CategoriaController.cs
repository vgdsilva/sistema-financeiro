using Microsoft.AspNetCore.Mvc;

namespace SistemaFinanceiro.Controllers;

public class CategoriaController : Controller
{
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public async Task<IActionResult> Create()
    {
        return View();
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        return View();
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        return View();
    }
}
