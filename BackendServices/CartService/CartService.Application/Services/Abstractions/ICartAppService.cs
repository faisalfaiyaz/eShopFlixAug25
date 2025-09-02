using CartService.Application.DTOs;

namespace CartService.Application.Services.Abstractions;

public interface ICartAppService
{
    Task<CartDTO> GetUserCart(long userId);
    int GetCartItemCount(long userId);
    IEnumerable<CartItemDTO> GetCartItems(long cartId);
    Task<CartDTO> GetCart(int cartId);
    CartDTO AddItem(long userId, long cartId, int itemId, decimal unitPrice, int quantity);
    int DeleteItem(int cartId, int itemId);
    bool MakeInActive(int cartId);
    int UpdateQuantity(int cartId, int itemId, int quantity);
}
