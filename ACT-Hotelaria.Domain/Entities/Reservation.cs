using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Reservation : BaseEntity
{
    public TypeRoomReservationEnum Type { get; private set; }
    public DateTime Checkin { get; private set; } = DateTime.UtcNow;
    public DateTime Checkout { get; private set; }
    public Price TotalPrice { get; private set; }
    
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }

    private Reservation()
    {
    }
    private Reservation(TypeRoomReservationEnum type, DateTime checkin, DateTime checkout, decimal dailyRate, Guid clientId)    {
        
        if (clientId == Guid.Empty)
            throw new ArgumentException("O cliente é obrigatório.");
        
        validateCheckin(checkin, checkout);
        
        Type = type;
        Checkin = checkin;
        Checkout = checkout;
        ClientId = clientId;
        
        CalculatePrice(dailyRate);
    }

    public static Reservation Create(TypeRoomReservationEnum type, DateTime checkin, DateTime checkout, decimal dailyRate, Guid clientId)
    {
        return new Reservation(type, checkin, checkout, dailyRate, clientId);
    }

    private void CalculatePrice(decimal dailyRate)
    {
        var days = (Checkout - Checkin).Days; 
        if (days <= 0) days = 1;
        TotalPrice = Price.Create(days * dailyRate);
    }

    private void validateCheckin(DateTime checkin, DateTime checkout)
    {
        if (checkout <= checkin)
            throw new ArgumentException("A data de checkout deve ser posterior ao checkin.");
        
        if (checkin < DateTime.UtcNow.Date)
            throw new ArgumentException("A data de checkin não pode ser no passado.");
    }
}