using System.Text.RegularExpressions;

namespace ACT_Hotelaria.Domain.ValueObject;

public sealed record Telefone
{
    private static readonly Regex NumberRegex = new Regex(@"[^\d]", RegexOptions.Compiled);
    public string Value { get; }
    public Telefone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value), "O telefone não pode ser vazio.");
        }
        
        var apenasNumeros = NumberRegex.Replace(value, "");
        
        if (apenasNumeros.Length != 11)
        {
            throw new ArgumentException("Telefone inválido: deve conter DDD + 9 dígitos.");
        }
        
        if (new string(apenasNumeros[0], 11) == apenasNumeros)
        {
            throw new ArgumentException("Telefone inválido: números repetidos.");
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