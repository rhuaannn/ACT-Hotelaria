using System.Text.RegularExpressions;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Domain.ValueObject;

public sealed record Cpf
{
    public string Value { get; }

    public Cpf(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(ResourceMessages.CPFObrigatorio);

        var apenasNumeros = Utils.SomenteNumeros(value);

        if (!ValidarCpf(apenasNumeros))
            throw new DomainException(ResourceMessages.CPFObrigatorio);

        Value = apenasNumeros;
    }
    public static Cpf Create(string value) => new Cpf(value);

    public static implicit operator Cpf(string value) => Create(value);
    public static implicit operator string(Cpf cpf) => cpf.Value;

    public override string ToString() => Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");
    private static bool ValidarCpf(string cpf)
    {
        if (cpf.Length != 11) return false;
        
        if (new string(cpf[0], 11) == cpf) return false;

        var tempCpf = cpf.Substring(0, 9);
        var soma = 0;

        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (10 - i);

        var resto = soma % 11;
        var digito = resto < 2 ? 0 : 11 - resto;

        if (int.Parse(cpf[9].ToString()) != digito) return false;

        tempCpf += digito;
        soma = 0;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        digito = resto < 2 ? 0 : 11 - resto;

        return int.Parse(cpf[10].ToString()) == digito;
    }
    private  static class Utils 
    {
        public static string SomenteNumeros(string text) => 
            string.IsNullOrEmpty(text) ? "" : Regex.Replace(text, @"[^\d]", "");
    }
}