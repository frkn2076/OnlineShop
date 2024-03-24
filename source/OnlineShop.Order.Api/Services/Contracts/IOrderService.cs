using OnlineShop.Order.Api.Models.Responses;

namespace OnlineShop.Order.Api.Services.Contracts;

public interface IOrderService
{
    Task<IEnumerable<OrderResponseModel>> GetOrdersAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default);
}
