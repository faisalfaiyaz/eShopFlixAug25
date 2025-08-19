using CatalogService.Domain.Entities;

namespace CatalogService.Application.Repositories;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product GetById(int id);
    IEnumerable<Product> GetByIds(int[] ids);
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
    int SaveChanges();
}
