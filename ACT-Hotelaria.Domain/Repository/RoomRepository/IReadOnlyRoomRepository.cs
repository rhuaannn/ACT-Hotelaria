using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.RoomRepository;

public interface IReadOnlyRoomRepository
{
    Task<Room> GetById(Guid id);
    Task<IEnumerable<Room>> GetAll();
    Task<bool> Exists(Guid id);
    Task<int> GetOccupancyCountAsync(Guid roomId, DateTime start, DateTime end);
}