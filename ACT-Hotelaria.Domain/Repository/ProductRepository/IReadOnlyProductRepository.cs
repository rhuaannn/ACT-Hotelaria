using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.ProductRepository;

public interface IReadOnlyProductRepository
{
    Task<Product> GetById(Guid id);
    Task<IEnumerable<Product>> GetAll();
    Task<bool> Exists(Guid id);
    
}