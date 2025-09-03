using CartService.Application.DTOs;
using CartService.Application.Services.Abstractions;
using CartService.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartService.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartAppService _cartService;
    public CartController(ICartAppService cartAppService)
    {
        _cartService = cartAppService;
    }

    [HttpGet("{UserId}")]
    public async Task<IActionResult> GetUserCart(long UserId)
    {
        var cart = await _cartService.GetUserCart(UserId);
        return Ok(cart);
    }

    [HttpPost("{UserId}")]
    public IActionResult AddItem(CartItem item, long UserId)
    {
        var cart = _cartService.AddItem(UserId, item.CartId, item.ItemId, item.UnitPrice, item.Quantity);
        return Ok(cart);
    }

    [HttpGet("{CartId}")]
    public async Task<IActionResult> GetCart(int CartId)
    {
        var cart = await _cartService.GetCart(CartId);
        return Ok(cart);
    }

    [HttpGet("{UserId}")]
    public IActionResult GetCartItemCount(int UserId)
    {
        var count = _cartService.GetCartItemCount(UserId);
        return Ok(count);
    }

    [HttpGet("{CartId}")]
    public IEnumerable<CartItemDTO> GetItems(int CartId)
    {
        return _cartService.GetCartItems(CartId);
    }

    [HttpGet("{CartId}")]
    public IActionResult MakeInActive(int CartId)
    {
        var status = _cartService.MakeInActive(CartId);
        return Ok(status);
    }

    [HttpDelete("{CartId}/{ItemId}")]
    public IActionResult DeleteItem(int CartId, int ItemId)
    {
        var count = _cartService.DeleteItem(CartId, ItemId);
        return Ok(count);
    }

    [HttpGet("{CartId}/{ItemId}/{Quantity}")]
    public IActionResult UpdateQuantity(int CartId, int ItemId, int Quantity)
    {
        var count = _cartService.UpdateQuantity(CartId, ItemId, Quantity);
        return Ok(count);
    }
}
