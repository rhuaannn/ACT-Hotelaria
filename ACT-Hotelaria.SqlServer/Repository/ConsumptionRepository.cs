using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.cs.ConsumptionRepository.cs;

namespace ACT_Hotelaria.SqlServer.Repository;

public class ConsumptionRepository : IWriteOnlyConsumptionRepository
{
    private readonly ACT_HotelariaDbContext _context;
    public ConsumptionRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task Add(Consumption consumption)
    {
        _context.Consumptions.Add(consumption);
        await _context.SaveChangesAsync();
    }

    public void Update(Consumption consumption)
    {
         _context.Consumptions.Update(consumption);
         _context.SaveChanges();
    }
}