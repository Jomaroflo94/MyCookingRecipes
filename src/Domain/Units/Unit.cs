using Domain.Shared;
using Primitives.Entities;

namespace Domain.Units;
public sealed class Unit : Entity
{
    private Unit(Ulid id, Text name, Text symbol, DateTime createdOnUtc) 
        : base(id, createdOnUtc) 
    {
        Name = name;
        Symbol = symbol;
    }

    private Unit() { }

    public Text Name { get; private set; }
    public Text Symbol { get; private set; }

    public static Unit Create(Ulid id, Text name, Text symbol,
        DateTime createdOnUtc)
    {
        return new Unit(id, name, symbol,
            createdOnUtc);
    }
}
