using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.ClientRepository;
using ACT_Hotelaria.Domain.ValueObject;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class ClientRepository : IReadOnlyClientRepository, IWriteOnlyClientRepository
{
    private readonly ACT_HotelariaDbContext _context;
    
    public ClientRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task<Client> GetById(Guid id)
    {
        var client = await _context.Clients
            .Include(c => c.Dependents)
            .Include(c => c.Reservations)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return client;
    }

    public async Task<IEnumerable<Client>> GetAll()
    {
        var clients = await _context.Clients
            .Include(c => c.Dependents)
            .Include(c => c.Reservations)
            .AsNoTracking() 
            .ToListAsync();        
        return clients;
         
    }

    public async Task Add(Client client)
    {
        await _context.Clients.AddAsync(client);
    }

    public async Task<bool> Remove(Guid id)
    {
        var exists = await _context.Clients.AnyAsync(c => c.Id == id);
        if (exists)
        {
            _context.Clients.Remove(await GetById(id));
            return true;
        }
        return false;
    }
    
    public async Task<bool> Exists(Guid id)
    {
        return await _context.Clients.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> ExistsCpf(Cpf cpf)
    {
        var exists =  _context.Clients.Any(c => c.CPF == cpf);
        if(exists) return await Task.FromResult(true);
        return await Task.FromResult(false);
    }

    public async Task<bool> ExistsCheckinPeriod(DateTime checkin, DateTime checkout)
    {
        var exists = _context.Reservations.Any(r => r.Checkin >= checkin && r.Checkout <= checkout);
        if(exists) return await Task.FromResult(true);
        return await Task.FromResult(false);
    }

    public void Update(Client client)
    {
       _context.Clients.Update(client);
    }
}
