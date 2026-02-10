using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.Exception;

namespace ACT_Hotelaria.Domain.Entities;

public class Room : BaseEntity
{
    private readonly List<Reservation> _reservations = new();
    public TypeRoomReservationEnum Type { get; private set; }
    public int QtyRoom { get; private set; }
    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();
    private Room() { }

    private Room(TypeRoomReservationEnum type, int qtyRoom)
    {
        ValidateRoom(type, qtyRoom);
        Type = type;
        QtyRoom = qtyRoom;
    }

    public static Room Create(TypeRoomReservationEnum type, int qtyRoom)
    {
        return new Room(type, qtyRoom);
    }

    private void ValidateRoom(TypeRoomReservationEnum type, int qtyRoom)
    {
        var existsEnum = System.Enum.IsDefined(typeof(TypeRoomReservationEnum), type);
        if (!existsEnum)
        {
            throw new DomainException("O tipo de quarto deve ser válido.");
        }
        if (qtyRoom <= 0)
        {
            throw new DomainException("A quantidade de quartos deve ser maior que zero.");
        }
    }
    
}