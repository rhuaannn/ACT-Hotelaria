namespace ACT_Hotelaria.Domain.ValueObject;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        ValidateEmail(value);
        if (!ValidateEmail(value))
        {
            throw new ArgumentException("Email inválido.");
        }
        Value = value;
    }

    public bool ValidateEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
    public static Email Create(string value) => new Email(value);

    public static implicit operator Email(string value) => Create(value);
    public static implicit operator string(Email email) => email.Value;
    public override string ToString() => Value;
    
}