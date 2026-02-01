using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.ClientRepository;

public interface IWriteOnlyClientRepository
{
    public Task Add(Client client);
    public Task<bool> Remove(Guid id);
    public void  Update(Client client);
    
}
