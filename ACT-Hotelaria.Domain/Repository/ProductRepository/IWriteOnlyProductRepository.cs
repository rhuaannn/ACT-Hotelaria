using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.ProductRepository;

public interface IWriteOnlyProductRepository
{
    Task Add(Product product);
    Task<bool> Remove(Guid id);
    void Update(Product product);
}