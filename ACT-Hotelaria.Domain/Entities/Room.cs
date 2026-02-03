using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Enum;

namespace ACT_Hotelaria.Domain.Entities;

public class Room : BaseEntity
{
    public TypeRoomReservationEnum Type { get; private set; }
    public int QtyRoom { get; private set; }
    
    private Room() { }

    private Room(TypeRoomReservationEnum type, int qtyRoom)
    {
        Type = type;
        QtyRoom = qtyRoom;
    }

    public static Room Create(TypeRoomReservationEnum type, int qtyRoom)
    {
        return new Room(type, qtyRoom);
    }
}