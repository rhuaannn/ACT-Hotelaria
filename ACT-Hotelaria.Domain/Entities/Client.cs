using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Client : BaseEntity
{
    private readonly List<Dependent> _dependent;
    public string Name { get; private set; }
    public Cpf CPF { get; private set; }
    public Email Email { get; private set; }
    public Telefone Telefone { get; private set; }
    public bool Active { get; private set; } = true;
    
    public IReadOnlyCollection<Dependent> Dependents => _dependent.AsReadOnly();

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
        _dependent = new List<Dependent>();
    }

    public static Client Create(string name, Cpf cpf, Email email, Telefone telefone)
    {
        return new Client(name, cpf, email, telefone);
    }

    public void AddDependent(string name, Cpf cpf)
    {
        if (_dependent.Any(d => d.CPF.Equals(cpf)))
        {
            throw new ArgumentException("Dependente já cadastrado!");
        }
        
        var newDependent = Dependent.Create(name, cpf);
        _dependent.Add(newDependent);
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