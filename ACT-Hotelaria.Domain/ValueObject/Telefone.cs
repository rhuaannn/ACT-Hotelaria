using System.Text.RegularExpressions;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Domain.ValueObject;

public sealed record Telefone
{
    private static readonly Regex NumberRegex = new Regex(@"[^\d]", RegexOptions.Compiled);
    public string Value { get; }
    public Telefone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException(ResourceMessages.TelefoneObrigatorio);
        }
        
        var apenasNumeros = NumberRegex.Replace(value, "");
        
        if (apenasNumeros.Length != 11)
        {
            throw new DomainException(ResourceMessages.TelefoneNumero);
        }
        
        if (new string(apenasNumeros[0], 11) == apenasNumeros)
        {
            throw new DomainException(ResourceMessages.TelefoneNumero);
        }

        Value = apenasNumeros;
    }
    public static Telefone Create (string value)
    {
       return new Telefone(value);
    }

    public static implicit operator Telefone(string value) => Create(value);
    public static implicit operator string(Telefone telefone) => telefone.Value;
    public override string ToString() => Value;
}