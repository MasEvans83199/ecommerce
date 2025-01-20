using Microsoft.AspNetCore.Mvc;
using ECommercePortfolio.Models;

namespace ECommercePortfolio.Controllers;

public class ProductsController : Controller
{
    public IActionResult Index()
    {
        var products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 19.99m, Description = "Description 1" },
            new Product { Id = 2, Name = "Product 2", Price = 29.99m, Description = "Description 2" },
            new Product { Id = 3, Name = "Product 3", Price = 39.99m, Description = "Description 3" },
        };
        return View(products);
    }
}