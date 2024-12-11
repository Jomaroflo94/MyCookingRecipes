using ROP;
using ROP.Extensions;

namespace Domain.Shared;
public sealed record PInt
{
    public PInt(int value) => Value = value;

    public int Value { get; }

    public static Result<PInt> Create(int? value)
    {
        return Result.Create(value)
            .Ensure(value => value > 0, PIntErrors.NegativeOrZero)
            .Map(value => new PInt(value ?? 0));
    }
}
