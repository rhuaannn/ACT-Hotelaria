using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Repository.DependentRepository;

public interface IReadOnlyDependentRepository
{
    public Task<Dependent> GetById(Guid id);
    public Task<IEnumerable<Dependent>> GetAll();
    public Task<bool> ExistsCpf(Cpf cpf);
}
