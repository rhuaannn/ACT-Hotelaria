using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Repository.ClientRepository;

public interface IReadOnlyClientRepository
{
    public Task<Client> GetById(Guid id);
    public Task<IEnumerable<Client>> GetAll();
    public Task<bool> Exists(Guid id);
    public Task<bool> ExistsCpf(Cpf cpf);
    public Task<bool> ExistsCheckinPeriod(DateTime checkin, DateTime checkout);

}
