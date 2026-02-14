using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ConsumptionRepository.cs;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class ConsumptionRepository : IWriteOnlyConsumptionRepository, IReadOnlyConsumptionRepository
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

    public async Task<IEnumerable<Consumption>> GetAllAsync()
    {
        var consumption = await _context.Consumptions
            .Include(c => c.Reservation).ToListAsync();
        return consumption;
    }

    public async Task<Consumption> GetByIdAsync(Guid id)
    {
        var consumption = await _context.Consumptions.FirstOrDefaultAsync(c => c.Id == id);
        return consumption;
    }
}