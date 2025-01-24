using ECommercePortfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommercePortfolio.Views.Shared.Services;

public class ShoppingCart
{
    private readonly ApplicationDbContext _context;
    private string ShoppingCartId { get; set; }
    
    public List<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ShoppingCart(ApplicationDbContext context)
    {
        _context = context;
    }

    public static ShoppingCart GetCart(IServiceProvider services)
    {
        ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
        var context = services.GetService<ApplicationDbContext>();
        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
        session.SetString("CartId", cartId);
        var cart = new ShoppingCart(context) { ShoppingCartId = cartId };
        cart.ShoppingCartItems = cart.GetShoppingCartItems();
        return cart;
    }

    public void AddToCart(Product product, int quantity)
    {
        var shoppingCartItem =
            _context.ShoppingCartItems.SingleOrDefault(s =>
                s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);

        if (shoppingCartItem == null)
        {
            shoppingCartItem = new ShoppingCartItem
            {
                ShoppingCartId = ShoppingCartId,
                Product = product,
                Quantity = quantity
            };
            _context.ShoppingCartItems.Add(shoppingCartItem);
        }
        else
        {
            shoppingCartItem.Quantity += quantity;
        }

        _context.SaveChanges();
    }

    public int RemoveFromCart(Product product)
    {
        var shoppingCartItem =
            _context.ShoppingCartItems.SingleOrDefault(s =>
                s.Product.Id == product.Id && s.ShoppingCartId == ShoppingCartId);
        var localAmount = 0;

        if (shoppingCartItem != null)
        {
            if (shoppingCartItem.Quantity > 1)
            {
                shoppingCartItem.Quantity--;
                localAmount = shoppingCartItem.Quantity;
            }
            else
            {
                _context.ShoppingCartItems.Remove(shoppingCartItem);
            }
        }

        _context.SaveChanges();

        return localAmount;
    }

    public List<ShoppingCartItem> GetShoppingCartItems()
    {
        return _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Include(s => s.Product)
            .ToList();
    }

    public void ClearCart()
    {
        var cartItems = _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId);

        _context.ShoppingCartItems.RemoveRange(cartItems);
        _context.SaveChanges();
    }

    public decimal GetShoppingCartTotal()
    {
        return _context.ShoppingCartItems
            .Where(c => c.ShoppingCartId == ShoppingCartId)
            .Select(c => c.Product.Price * c.Quantity)
            .Sum();
    }
}