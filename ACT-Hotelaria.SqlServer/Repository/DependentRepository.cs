using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.DependentRepository;
using ACT_Hotelaria.Domain.ValueObject;
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

    public async Task<bool> ExistsCpf(Cpf cpf)
    {
        var exists = await _context.Dependents.AnyAsync(d => d.CPF == cpf);
        if(exists) return true;
        return false;
    }
}