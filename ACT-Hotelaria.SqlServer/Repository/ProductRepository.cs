using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.cs.ProductRepository;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class ProductRepository : IReadOnlyProductRepository, IWriteOnlyProductRepository
{
    private readonly ACT_HotelariaDbContext _context;
    
    public ProductRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public Task<Product> GetById(Guid id)
    {
        var product = _context.Products
                                .AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);
        return product;
    }

    public Task<IEnumerable<Product>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Exists(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public Task<bool> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        throw new NotImplementedException();
    }
}