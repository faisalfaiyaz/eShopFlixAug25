using CatalogService.Application.Repositories;
using CatalogService.Domain.Entities;

namespace CatalogService.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogServiceDbContext _db;

    public ProductRepository(CatalogServiceDbContext db)
    {
        _db = db;
    }

    public void Add(Product product)
    {
        _db.Products.Add(product);
    }

    public void Delete(int id)
    {
        var product = _db.Products.Find(id);
        if (product != null)
        {
            _db.Products.Remove(product);
        }
    }

    public IEnumerable<Product> GetAll()
    {
        return _db.Products.ToList();
    }

    public Product GetById(int id)
    {
        return _db.Products.Find(id)!;
    }

    public IEnumerable<Product> GetByIds(int[] ids)
    {
        return _db.Products.Where(p => ids.Contains(p.ProductId)).ToList();
    }

    public int SaveChanges()
    {
        return _db.SaveChanges();
    }

    public void Update(Product product)
    {
        _db.Products.Update(product);
    }
}
