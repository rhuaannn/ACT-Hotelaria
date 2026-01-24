using ACT_Hotelaria.Domain.Abstract;
using ACT_Hotelaria.Domain.ValueObject;

namespace ACT_Hotelaria.Domain.Entities;

public sealed class Dependent : BaseEntity
{
    public string Name { get; private set; }
    public Cpf CPF { get; private set; }

    private Dependent()
    {
    }
    private Dependent(string name, Cpf cpf)
    {
        Name = name;
        CPF = cpf;
    }
    public static Dependent Create(string name, Cpf cpf)
    {
        return new Dependent(name, cpf);
    }
    
}