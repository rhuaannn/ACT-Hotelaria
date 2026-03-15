using ACT_Hotelaria.Domain.Entities;
using ACT_Hotelaria.Domain.Exception;
 using ACT_Hotelaria.Domain.Repository.RoomRepository;
using Microsoft.EntityFrameworkCore;

namespace ACT_Hotelaria.SqlServer.Repository;

public class RoomRepository : IReadOnlyRoomRepository, IWriteOnlyRoomRepository
{
    private readonly ACT_HotelariaDbContext _context;
    
    public RoomRepository(ACT_HotelariaDbContext context)
    {
        _context = context;
    }
    
    public async Task<Room> GetById(Guid id)
    {
       var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
       return room;
     
    }

    public async Task<IEnumerable<Room>> GetAll()
    {
        var rooms = await _context.Rooms.ToListAsync();
        return rooms;
    }

    public async Task<bool> Exists(Guid id)
    {
        var exists = await _context.Rooms.AnyAsync(r => r.Id == id);
        return exists;
    }

    public async Task<int> GetOccupancyCountAsync(Guid roomId, DateTime start, DateTime end)
    {
        return await _context.Reservations
            .AsNoTracking()
            .CountAsync(r => 
                r.RoomId == roomId && 
                r.Checkout > start && 
                r.Checkin < end
            );
    }

    public async Task Add(Room room)
    {
        await _context.AddAsync(room);
    }

    public Task Update(Room room)
    {
        throw new NotImplementedException();
    }
}