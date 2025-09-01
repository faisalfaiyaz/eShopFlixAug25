using CartService.Domain.Entities;

namespace CartService.Application.Repositories;

public interface ICartRepository
{
    Cart GetUserCart(long UserId);
    int GetCartItemCount(long UserId);
    IEnumerable<CartItem> GetCartItems(long CartId);
    Cart GetCart(long CartId);
    Cart AddItem(long CartId, long UserId, CartItem item);
    int DeleteItem(long CartId, int ItemId);
    bool MakeInActive(long CartId);
    int UpdateQuantity(long CartId, int ItemId, int Quantity);
}