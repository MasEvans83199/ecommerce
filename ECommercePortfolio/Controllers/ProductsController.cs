using Microsoft.AspNetCore.Mvc;
using ECommercePortfolio.Models;

namespace ECommercePortfolio.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}