using Microsoft.AspNetCore.Mvc;
using OnlineShop.Order.Api.Models.Requests;
using OnlineShop.Order.Api.Models.Responses;
using OnlineShop.Order.Api.Services.Contracts;

namespace OnlineShop.Order.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponseModel>))]
    public async Task<IActionResult> GetOrdersAsync([FromQuery] OrderRequest request, CancellationToken cancellationToken)
    {
        var orders = await orderService.GetOrdersAsync(request.From, request.To, cancellationToken);
        return Ok(orders);
    }
}
