namespace OnlineShop.Order.Api.Models.Responses;

public class OrderResponseModel
{
    public int Id { get; set; }

    public decimal Amount { get; set; }

    public DateTime OrderedAt { get; set; }

    public CustomerResponseModel Customer { get; set; }

    public ProductResponseModel[] Products { get; set; }
}
