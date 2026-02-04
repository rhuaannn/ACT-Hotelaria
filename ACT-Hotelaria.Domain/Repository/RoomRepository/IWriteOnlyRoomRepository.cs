using ACT_Hotelaria.Domain.Entities;

namespace ACT_Hotelaria.Domain.Repository.RoomRepository;

public interface IWriteOnlyRoomRepository
{
    Task Add (Room room);
    Task Update(Room room);
}