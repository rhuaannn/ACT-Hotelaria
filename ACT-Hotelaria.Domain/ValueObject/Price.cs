namespace ACT_Hotelaria.Domain.ValueObject;

public sealed record Price
{
    public decimal Value { get; set; }

    public Price(decimal value)
    {
        validate(value);
        Value = value;
    }
    
    public static Price Create(decimal value) => new Price(value);

    public bool validate(decimal price)
    {
        if (price <= 0)
        {
            throw new ArgumentException("Preço deve ser maior que zero.");
        }
        return true;
    }
    
    public static implicit operator decimal(Price price) => price.Value;
    public static implicit operator Price(decimal value) => Create(value);
    public override string ToString() => Value.ToString("f2");
}