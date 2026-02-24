using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ProductRepository;
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

    public async Task<IEnumerable<Product>> GetAll()
    {
       var productAll = await _context.Products.ToListAsync();
       return productAll;
    }

    public Task<bool> Exists(Guid id)
    {
       var existis = _context.Products.AnyAsync(p => p.Id == id);
       return existis;
    }

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
    }

    public Task<bool> Remove(Guid id)
    {
        throw new NotImplementedException();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
}