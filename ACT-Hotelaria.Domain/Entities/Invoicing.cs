using ACT_Hotelaria.Domain.Abstract;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Invoicing : BaseEntity
{
    public decimal TotalRoomValue { get; private set; }   
    public decimal TotalConsumptionValue { get; private set; } 
    public decimal ValueTotal { get; private set; }      
    public DateTime IssueDate { get; private set; }
    public Guid ReservationId { get; private set; }
    
    private Invoicing()
    {
    }

    private Invoicing(Guid reservationId, decimal roomValue, decimal consumptionValue)
    {
        Id = Guid.NewGuid();
        IssueDate = DateTime.UtcNow;
        ReservationId = reservationId;
        TotalRoomValue = roomValue;
        TotalConsumptionValue = consumptionValue;
        
        ValueTotal = TotalRoomValue + TotalConsumptionValue;
    }
    public static Invoicing Create(Reservation reservation)
    {
        var roomTotal = reservation.CalculateTotalPrice();
        var consumptionTotal = reservation.Consumptions.Sum(x => x.TotalValue);

        return new Invoicing(reservation.Id, roomTotal, consumptionTotal);
    }
}