using ACT_Hotelaria.Domain.Abstract;

namespace ACT_Hotelaria.Domain.Entities;

public class Consumption : BaseEntity
{
    public int QtyProduct { get; private set; }
    public decimal UnitPrice { get; private set; }
    public Guid ReservationId { get; private set; }
    public Guid ProductId { get; private set; }
    
    public decimal TotalValue => UnitPrice * QtyProduct;
    
    public Reservation Reservation { get; private set; }
    public Product Product { get; private set; }

    private Consumption()
    {
    }
    private Consumption(Guid reservationId, Guid productId, int qtyProduct, decimal currentPrice)
    {
        ReservationId = reservationId;
        ProductId = productId;
        QtyProduct = qtyProduct;
        UnitPrice = currentPrice;
    }

    internal static Consumption Create(Guid reservationId, Guid productId, int qtyProduct, decimal currentPrice)
    {
        return new Consumption(reservationId, productId, qtyProduct, currentPrice);
    }


}