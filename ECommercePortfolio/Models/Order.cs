namespace ECommercePortfolio.Models;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string UserId { get; set; }
    public decimal TotalAmount { get; set; }
}