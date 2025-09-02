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
    public Cart AddItem(long cartId, long userId, CartItem item)
    {
        Cart cart = new Cart();

        // If cartId is provided, find by cartId, else find by userId
        if (cartId > 0)
            cart = _db.Carts.Find(cartId);
        else
            cart = _db.Carts.FirstOrDefault(c => c.UserId == userId && c.IsActive == true);


        // If cart exists, than add Cart-item to it, else create a new cart
        if (cart != null)
        {
            // Check if item already exists in cart, if yes then update quantity
            CartItem cartItem = _db.CartItems.FirstOrDefault(c => c.ItemId == item.ItemId && c.CartId == cart.Id);
            if (cartItem != null)
            {
                // Update quantity of existing item in cart 
                cartItem.Quantity += item.Quantity;
                _db.SaveChanges();
                return cart;
            }
            else
            {
                // Add new item to cart 
                cart.CartItems.Add(item);
                _db.SaveChanges();
                return cart;
            }
        }
        else
        {
            // Create a new cart and add item to it 
            cart = new Cart
            {
                UserId = userId,
                CreatedDate = DateTime.Now,
                IsActive = true
            };

            // Add item to cart
            cart.CartItems.Add(item);

            // add cart to database and save changes
            _db.Carts.Add(cart);
            _db.SaveChanges();
            return cart;
        }
    }

    public int DeleteItem(long cartId, int itemId)
    {
        // Using ExecuteDelete for better performance instead of fetching the entity and then deleting it
        return _db.CartItems.Where(ci => ci.Id == itemId && ci.CartId == cartId).ExecuteDelete();
    }

    public Cart GetCart(long cartId)
    {
        // Include CartItems when fetching the cart details for better performance and to avoid lazy loading issues later on 
        return _db.Carts.Include(c => c.CartItems)
                        .FirstOrDefault(c => c.Id == cartId && c.IsActive)!;
    }

    public int GetCartItemCount(long userId)
    {
        // Include CartItems when fetching the cart details for better performance and to avoid lazy loading issues later on
        Cart cart = _db.Carts.Include(c => c.CartItems)
                             .FirstOrDefault(c => c.UserId == userId && c.IsActive)!;


        // If cart exists, then return the count of items in the cart, else return 0
        if (cart != null)
        {
            int counter = cart.CartItems.Sum(c => c.Quantity);
            return counter;
        }
        return 0;
    }

    public IEnumerable<CartItem> GetCartItems(long cartId)
    {
        // Fetch all cart items for the given cartId
        return _db.CartItems.Where(ci => ci.CartId == cartId).ToList();
    }

    public Cart GetUserCart(long userId)
    {
        // Include CartItems when fetching the cart details for better performance and to avoid lazy loading issues later on
        return _db.Carts.Include(c => c.CartItems)
                        .FirstOrDefault(c => c.UserId == userId && c.IsActive == true)!;
    }

    public bool MakeInActive(long cartId)
    {
        // Find cart by cartId and mark it as inactive
        Cart cart = _db.Carts.Find(cartId)!;
        if (cart != null)
        {
            // Mark cart as inactive
            cart.IsActive = false;
            _db.SaveChanges();
            return true;
        }
        return false;
    }

    public int UpdateQuantity(long cartId, int itemId, int quantity)
    {
        // Find cart item by itemId and cartId, if found then update the quantity
        CartItem cartItem = _db.CartItems.FirstOrDefault(ci => ci.Id == itemId && ci.CartId == cartId)!;
        if (cartItem != null)
        {
            // Update quantity of existing item in cart
            cartItem.Quantity += quantity;
            _db.SaveChanges();
            return 1;
        }
        return 0;
    }
}