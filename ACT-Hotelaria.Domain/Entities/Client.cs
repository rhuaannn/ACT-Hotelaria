using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.Exception;
using ACT_Hotelaria.Domain.ValueObject;
using ACT_Hotelaria.Message;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Client : BaseEntity
{
    private readonly List<Dependent> _dependents = new();
    private readonly List<Reservation> _reservations = new();
    public string Name { get; private set; }
    public Cpf CPF { get; private set; }
    public Email Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public bool Active { get; private set; } = true;
    
    public IReadOnlyCollection<Dependent> Dependents => _dependents.AsReadOnly();
    public IReadOnlyCollection<Reservation> Reservations => _reservations.AsReadOnly();

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
        Active = true;
    }

    public static Client Create(string name, Cpf cpf, Email email, Telefone telefone)
    {
        return new Client(name, cpf, email, telefone);
    }

    public void AddDependent(string name, Cpf cpf)
    {
        if (_dependents.Any(d => d.CPF.Equals(cpf)))
        {
            throw new ConflitException(ResourceMessages.DependenteJaCadastrado);
        }
        
        var newDependent = Dependent.Create(name, cpf);
        _dependents.Add(newDependent);
    }
    private bool Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 3)
        {
            throw new DomainException(ResourceMessages.NomeObrigatorio);
        }

        return true;
    }
}