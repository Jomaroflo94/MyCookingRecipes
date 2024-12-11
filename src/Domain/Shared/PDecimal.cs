using ROP;
using ROP.Extensions;

namespace Domain.Shared;
public sealed record PDecimal
{
    public PDecimal(decimal value) => Value = value;

    public decimal Value { get; }

    public static Result<PDecimal> Create(decimal? value)
    {
        return Result.Create(value)
            .Ensure(value => value > 0, PDecimalErrors.NegativeOrZero)
            .Map(value => new PDecimal(value ?? 0));
    }
}
