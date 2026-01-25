using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.DependentRepository;

public interface IReadOnlyDependentRepository
{
    public Task<Dependent> GetById(Guid id);
    public Task<IEnumerable<Dependent>> GetAll();
}
