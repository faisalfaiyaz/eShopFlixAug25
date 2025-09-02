using CartService.Domain.Entities;

namespace CartService.Application.Repositories;

public interface ICartRepository
{
    Cart GetUserCart(long userId);
    int GetCartItemCount(long userId);
    IEnumerable<CartItem> GetCartItems(long cartId);
    Cart GetCart(long cartId);
    Cart AddItem(long cartId, long userId, CartItem item);
    int DeleteItem(long cartId, int itemId);
    bool MakeInActive(long cartId);
    int UpdateQuantity(long cartId, int itemId, int quantity);
}