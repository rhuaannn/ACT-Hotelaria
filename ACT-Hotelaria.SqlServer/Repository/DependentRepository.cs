using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class DependentRepository : IReadOnlyDependentRepository
{
    private readonly ACT_HotelariaDbContext _context;
    
    public DependentRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task<Dependent> GetById(Guid id)
    {
        var dependent = await _context.Dependents
            .Include(d => d.Client)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
        return dependent;
    }

    public async Task<IEnumerable<Dependent>> GetAll()
    {
        var dependents = await _context.Dependents
            .Include(d => d.Client)
            .AsNoTracking()
            .ToListAsync();
        return dependents;
    }
}