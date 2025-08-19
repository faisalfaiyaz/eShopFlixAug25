using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstractions;
using CatalogService.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace CatalogService.Application.Services.Implementations;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    readonly string _imageServiceUrl;

    public ProductAppService(IProductRepository productRepository, IMapper mapper, IConfiguration configuration)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _configuration = configuration;
        _imageServiceUrl = _configuration["ImageServer"];
    }

    public void Add(ProductDTO product)
    {
        _productRepository.Add(_mapper.Map<Product>(product));
        _productRepository.SaveChanges();
    }

    public void Delete(int id)
    {
        _productRepository.Delete(id);
        _productRepository.SaveChanges();
    }

    public IEnumerable<ProductDTO> GetAll()
    {
        var products =_productRepository.GetAll();
        if (products != null)
        {
            products = products.Select(p =>
            {
                p.ImageUrl = $"{_imageServiceUrl}{p.ImageUrl}";
                return p;
            });

            // Map the products to ProductDTO
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        // If no products are found, return an empty collection
        return Enumerable.Empty<ProductDTO>();
    }

    public ProductDTO GetById(int id)
    {
        Product product = _productRepository.GetById(id);
        if(product != null)
        {
            // Set the ImageUrl property to include the image service URL
            product.ImageUrl = $"{_imageServiceUrl}{product.ImageUrl}";

            // Map the product to ProductDTO
            return _mapper.Map<ProductDTO>(product);
        }

        return null;
    }

    public IEnumerable<ProductDTO> GetByIds(int[] ids)
    {
        var products = _productRepository.GetByIds(ids);
        if (products != null)
        {
            products = products.Select(p =>
            {
                p.ImageUrl = $"{_imageServiceUrl}{p.ImageUrl}";
                return p;
            });

            // Map the products to ProductDTO
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        return Enumerable.Empty<ProductDTO>();
    }

    public void Update(ProductDTO product)
    {
        _productRepository.Update(_mapper.Map<Product>(product));
        _productRepository.SaveChanges();
    }
}
