using CatalogService.Application.DTOs;

namespace CatalogService.Application.Services.Abstractions;

public interface IProductAppService
{
    IEnumerable<ProductDTO> GetAll();
    ProductDTO GetById(int id);
    IEnumerable<ProductDTO> GetByIds(int[] ids);
    void Add(ProductDTO product);
    void Update(ProductDTO product);
    void Delete(int id);
}
