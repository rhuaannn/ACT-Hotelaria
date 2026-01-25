using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.ClientRepository;

public interface IReadOnlyClientRepository
{
    public Task<Client> GetById(Guid id);
    public Task<IEnumerable<Client>> GetAll();
    public Task<bool> Exists(Guid id);
    
}
