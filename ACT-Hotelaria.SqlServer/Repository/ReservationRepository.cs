using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Repository.cs.Reservation;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class ReservationRepository : IReadOnlyReservationRepository, IWriteOnlyReservationRepository
{
    private readonly ACT_HotelariaDbContext _context;

    public ReservationRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task<Reservation> GetAllById(Guid id)
    {
        var reservation = await _context.Reservations
            .Include(r => r.Client)
            .Include(r => r.Checkin)
            .Include(r => r.Checkout)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
        return reservation;
    }

    public async Task<IEnumerable<Reservation>> GetAll()
    {
        return await _context.Reservations
            .Include(r => r.Client)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> Exists(Guid id)
    {
        var exists = await _context.Reservations.AnyAsync(r => r.Id == id);
        if (exists) return true;
        return false;
    }

    public async Task<bool> ExistsCheckin(DateTime checkin)
    {
        var exists = await _context.Reservations.AnyAsync(r => r.Checkin == checkin);
        if(exists) return true;
        return false;
    }

    public async Task<bool> ExistsCheckout(DateTime checkout)
    {
        var exists = await _context.Reservations.AnyAsync(r => r.Checkout == checkout);
        if(exists) return true;
        return false;
    }

    public Task<IEnumerable<Client>> GetAllClient()
    {
        throw new NotImplementedException();
    }

    public async Task Add(Reservation reservation)
    {
        await _context.Reservations.AddAsync(reservation);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Remove(Guid id)
    {
        var remove = await _context.Reservations.AnyAsync(r => r.Id == id);
        if (remove)
        {
            _context.Reservations.Remove(await GetAllById(id));
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public void Update(Reservation reservation)
    {
        throw new NotImplementedException();
    }
}