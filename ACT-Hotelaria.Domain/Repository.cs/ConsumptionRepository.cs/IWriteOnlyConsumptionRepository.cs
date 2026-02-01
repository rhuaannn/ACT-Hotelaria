using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.cs.ConsumptionRepository.cs;

public interface IWriteOnlyConsumptionRepository
{
    Task Add(Consumption consumption);
    void Update(Consumption consumption);
}