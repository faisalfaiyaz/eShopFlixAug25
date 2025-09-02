using AutoMapper;
using CartService.Application.DTOs;
using CartService.Application.Repositories;
using CartService.Application.Services.Abstractions;
using CartService.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CartService.Application.Services.Implementations;

public class CartAppService : ICartAppService
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    public CartAppService(ICartRepository cartRepository, IMapper mapper, IConfiguration configuration)
    {
        _cartRepository = cartRepository;
        _mapper = mapper;
        _configuration = configuration;
    }


    public CartDTO AddItem(long userId, long cartId, int itemId, decimal unitPrice, int quantity)
    {
        //  add the item to the cart using the repository
        CartItem item = new CartItem
        {
            CartId = cartId,
            ItemId = itemId,
            UnitPrice = unitPrice,
            Quantity = quantity
        };

        Cart cart = _cartRepository.AddItem(cartId, userId, item);
        return _mapper.Map<CartDTO>(cart);
    }

    public int DeleteItem(int cartId, int itemId)
    {
        return _cartRepository.DeleteItem(cartId, itemId);
    }
    public Task<CartDTO> GetCart(int cartId)
    {
        return Task.FromResult(_mapper.Map<CartDTO>(_cartRepository.GetCart(cartId)));
    }

    public int GetCartItemCount(long userId)
    {
        return (userId > 0) ? _cartRepository.GetCartItemCount(userId) : 0;
    }

    public IEnumerable<CartItemDTO> GetCartItems(long cartId)
    {
        return (cartId > 0) ? _mapper.Map<IEnumerable<CartItemDTO>>(_cartRepository.GetCartItems(cartId)) : Enumerable.Empty<CartItemDTO>();
    }

    public Task<CartDTO> GetUserCart(long userId)
    {
        Cart cart = (userId > 0) ? _cartRepository.GetUserCart(userId) : null;
        return (cart is not null) ? Task.FromResult(_mapper.Map<CartDTO>(cart)) : Task.FromResult<CartDTO>(null);
    }

    public bool MakeInActive(int cartId)
    {
        return (cartId > 0) ? _cartRepository.MakeInActive(cartId) : false;
    }

    public int UpdateQuantity(int cartId, int itemId, int quantity)
    {
        return _cartRepository.UpdateQuantity(cartId, itemId, quantity);
    }

    private CartDTO PopulateCartDetails(Cart cart)
    {
        try
        {
            CartDTO cartModel = _mapper.Map<CartDTO>(cart);

            var productIds = cart.CartItems.Select(x => x.ItemId).ToArray();
            var products = _catalogServiceClient.GetByIdsAsync(productIds).Result;

            if (cartModel.CartItems.Count > 0)
            {
                cartModel.CartItems.ForEach(x =>
                {
                    var product = products.FirstOrDefault(p => p.ProductId == x.ItemId);
                    if (product != null)
                    {
                        x.Name = product.Name;
                        x.ImageUrl = product.ImageUrl;
                    }
                });

                foreach (var item in cartModel.CartItems)
                {
                    cartModel.Total += item.UnitPrice * item.Quantity;
                }
                cartModel.Tax = cartModel.Total * Convert.ToDecimal(_configuration["Tax"]) / 100;
                cartModel.GrandTotal = cartModel.Total + cartModel.Tax;
            }
            return cartModel;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
