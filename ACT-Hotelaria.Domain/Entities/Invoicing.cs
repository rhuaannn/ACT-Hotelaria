using ACT_Hotelaria.Domain.Abstract;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Invoicing : BaseEntity
{
    public decimal ValueTotal { get; private set; }
    
    public Guid ReservationId { get; private set; }
    public Reservation Reservation { get; private set; }

    private Invoicing()
    {
    }
    private Invoicing(decimal valueTotal, Guid reservationId) 
    {
        ValueTotal = valueTotal;
        ReservationId = reservationId;
    }
    
    public static Invoicing Create(decimal value, Guid reservationId)
    {
        return new(value, reservationId);
    }
}