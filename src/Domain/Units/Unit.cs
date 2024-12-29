using Domain.Shared;
using Primitives.Entities;

namespace Domain.Units;
public sealed class Unit : Entity
{
    public Unit(Ulid id, Text name, Text symbol, DateTime createdOnUtc) 
        : base(id, createdOnUtc) 
    {
        Name = name;
        Symbol = symbol;
    }

    private Unit() { }

    public Text Name { get; private set; }
    public Text Symbol { get; private set; }
}
