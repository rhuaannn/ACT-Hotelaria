using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;

public interface IReadOnlyConsumptionRepository
{
    public Task<IEnumerable<Consumption>> GetAllAsync();
    Task<Consumption> GetByIdAsync(Guid id);
};