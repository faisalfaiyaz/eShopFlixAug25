using CartService.Application.Repositories;
using CartService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CartService.Infrastructure.Persistence.Repositories;

public class CartRepository : ICartRepository
{
    CartServiceDbContext _db;
    public CartRepository(CartServiceDbContext db)
    {
        _db = db;
    }
    public Cart AddItem(long CartId, long UserId, CartItem item)
    {
        Cart cart = new Cart();
        if (CartId > 0)
            cart = _db.Carts.Find(CartId);
        else
            cart = _db.Carts.Where(x => x.UserId == UserId && x.IsActive == true).FirstOrDefault();

        if (cart != null)
        {
            CartItem cartItem = _db.CartItems.Where(x => x.ItemId == item.ItemId && x.CartId == cart.Id).FirstOrDefault();
            if (cartItem != null)
            {
                cartItem.Quantity += item.Quantity;
                _db.SaveChanges();
                return cart;
            }
            else
            {
                cart.CartItems.Add(item);
                _db.SaveChanges();
                return cart;
            }
        }
        else
        {
            cart = new Cart
            {
                UserId = UserId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            cart.CartItems.Add(item);
            _db.Carts.Add(cart);
            _db.SaveChanges();
            return cart;
        }
    }

    public int DeleteItem(long CartId, int ItemId)
    {
        return _db.CartItems.Where(x => x.Id == ItemId && x.CartId == CartId).ExecuteDelete();
    }

    public Cart GetCart(long CartId)
    {
        return _db.Carts.Include(x => x.CartItems).Where(x => x.Id == CartId && x.IsActive).FirstOrDefault();
    }

    public int GetCartItemCount(long UserId)
    {
        Cart cart = _db.Carts.Where(c => c.UserId == UserId && c.IsActive).Include(c => c.CartItems).FirstOrDefault();
        if (cart != null)
        {
            int counter = cart.CartItems.Sum(c => c.Quantity);
            return counter;
        }
        return 0;
    }

    public IEnumerable<CartItem> GetCartItems(long CartId)
    {
        return _db.CartItems.Where(x => x.CartId == CartId).ToList();
    }

    public Cart GetUserCart(long UserId)
    {
        return _db.Carts.Include(x => x.CartItems).Where(x => x.UserId == UserId && x.IsActive == true).FirstOrDefault();
    }

    public bool MakeInActive(long CartId)
    {
        Cart cart = _db.Carts.Find(CartId);
        if (cart != null)
        {
            cart.IsActive = false;
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public int UpdateQuantity(long CartId, int ItemId, int Quantity)
    {
        CartItem cartItem = _db.CartItems.Where(x => x.Id == ItemId && x.CartId == CartId).FirstOrDefault();
        if (cartItem != null)
        {
            cartItem.Quantity += Quantity;
            _db.SaveChanges();
            return 1;
        }
        return 0;
    }
}