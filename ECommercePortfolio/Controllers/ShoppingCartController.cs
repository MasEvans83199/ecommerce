using ECommercePortfolio.Views.Shared;
using ECommercePortfolio.Views.Shared.Services;
using Microsoft.AspNetCore.Mvc;

public class ShoppingCartController : Controller
{
    private readonly ShoppingCart _shoppingCart;
    private readonly ApplicationDbContext _context;

    public ShoppingCartController(ShoppingCart shoppingCart, ApplicationDbContext context)
    {
        _shoppingCart = shoppingCart;
        _context = context;
    }

    public ViewResult Index()
    {
        var items = _shoppingCart.GetShoppingCartItems();
        _shoppingCart.ShoppingCartItems = items;

        var viewModel = new ShoppingCartViewModel
        {
            ShoppingCart = _shoppingCart,
            ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
        };

        return View(viewModel);
    }

    public RedirectToActionResult AddToShoppingCart(int productId)
    {
        var selectedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (selectedProduct != null)
        {
            _shoppingCart.AddToCart(selectedProduct, 1);
        }
        return RedirectToAction("Index", "ShoppingCart");
    }

    public RedirectToActionResult RemoveFromShoppingCart(int productId)
    {
        var selectedProduct = _context.Products.FirstOrDefault(p => p.Id == productId);

        if (selectedProduct != null)
        {
            _shoppingCart.RemoveFromCart(selectedProduct);
        }
        return RedirectToAction("Index");
    }
}