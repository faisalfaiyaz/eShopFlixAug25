using AutoMapper;
using CatalogService.Application.DTOs;
using CatalogService.Application.Repositories;
using CatalogService.Application.Services.Abstractions;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services.Implementations;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ProductAppService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
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
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        return Enumerable.Empty<ProductDTO>();
    }

    public ProductDTO GetById(int id)
    {
        Product product = _productRepository.GetById(id);
        if(product != null)
        {
            return _mapper.Map<ProductDTO>(product);
        }

        return null;
    }

    public IEnumerable<ProductDTO> GetByIds(int[] ids)
    {
        var products = _productRepository.GetByIds(ids);
        if (products != null)
        {
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
