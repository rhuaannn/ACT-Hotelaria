using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; }
    public int QtyProduct { get; private set; }
    public Price ValueProduct { get; private set; }

    private Product()
    {
    }
    private Product(string name, int qtyProduct, Price valueProduct)
    {
        ValidateProduct(name, qtyProduct);
        Name = name;
        QtyProduct = qtyProduct;
        ValueProduct = valueProduct;
    }

    public static Product Create(string name, int qtyProduct, Price valueProduct)
    {
        return new Product(name, qtyProduct, valueProduct);
    }
    public void ReduceStock(int qtyProduct)
    {
        if (qtyProduct > QtyProduct) throw new DomainException("Estoque insuficiente.");
        QtyProduct -= qtyProduct;
    }
    private void ValidateProduct(string name, int qtyProduct)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new DomainException(ResourceMessages.NomeObrigatorio);
        }

        if (qtyProduct <= 0)
        {
            throw new DomainException(ResourceMessages.PrecoMaiorQueZero);
        }
        
    }
}