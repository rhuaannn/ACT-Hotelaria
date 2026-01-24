using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Client : BaseEntity
{
    public string Name { get; private set; }
    public Cpf CPF { get; private set; }
    public Email Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public bool Active { get; private set; } = true;

    private Client()
    {
    }
    private Client(string name, Cpf cpf, Email email, Telefone telefone)
    {
        Validate(name);
        Name = name;
        CPF = cpf;
        Email = email;
        Telefone = telefone;
    }

    public static Client Create(string name, Cpf cpf, Email email, Telefone telefone)
    {
        return new Client(name, cpf, email, telefone);
    }

    private bool Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
        {
            throw new ArgumentException("Não precisa ser válido!");
        }

        return true;
    }
}