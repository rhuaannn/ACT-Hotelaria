using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Enum;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Reservation : BaseEntity
{
    private readonly List<Consumption> _consumptions = new();
    public DateTime Checkin { get; private set; } = DateTime.UtcNow;
    public DateTime Checkout { get; private set; }
    public decimal AgreedDailyRate { get; private set; }
    public Guid ClientId { get; private set; }
    public Client Client { get; private set; }
    public Guid RoomId { get; private set; }
    public Room Room { get; private set; }

    public IReadOnlyCollection<Consumption> Consumptions => _consumptions.AsReadOnly();
    private Reservation()
    {
    }
    private Reservation(Guid roomId, DateTime checkin, DateTime checkout, Guid clientId, decimal agreedDailyRate)    {
        
        if (clientId == Guid.Empty)
            throw new DomainException(ResourceMessages.ClienteObrigatorio);
        
        validateCheckin(checkin, checkout);
        RoomId = roomId;
        Checkin = checkin;
        Checkout = checkout;
        ClientId = clientId;
        AgreedDailyRate = agreedDailyRate;
        
    }

    public static Reservation Create(Guid RoomId, DateTime checkin, DateTime checkout, 
                                     Guid clientId, decimal agreedDailyRate)
    {
        return new Reservation(RoomId, checkin, checkout, clientId, agreedDailyRate);
    }

    internal decimal CalculateTotalPrice()
    {
        var days = (Checkout - Checkin).Days; 
        if (days <= 0) days = 1;
    
        return days * AgreedDailyRate;
    }
    
    public void AddConsumption(Product product, int qtyRequested)
    {
        if (qtyRequested <= 0)
        {
            throw new DomainException(ResourceMessages.PrecoMaiorQueZero);
        }
        
        if (DateTime.UtcNow.Date > Checkout.Date)
        {
            throw new DomainException(ResourceMessages.CheckoutObrigatorio);
        }

        var consumption = Consumption.Create(this.Id, product.Id, qtyRequested, product.ValueProduct.Value);
         _consumptions.Add(consumption);
        
         product.ReduceStock(qtyRequested);
    }
    

    private void validateCheckin(DateTime checkin, DateTime checkout)
    {
        if (checkout <= checkin)
            throw new DomainException(ResourceMessages.CheckinAndCheckoutDiferente);
        
        if (checkin < DateTime.UtcNow.Date)
            throw new DomainException(ResourceMessages.CheckinObrigatorio);
    }
}