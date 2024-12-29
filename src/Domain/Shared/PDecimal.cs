using Primitives.Guards;

namespace Domain.Shared;
public sealed record PDecimal
{
    public PDecimal(decimal value) => Value = value;

    public decimal Value { get; }

    public static PDecimal Create(decimal? value)
    {
        Ensure.NotNull(value);
        Ensure.GreaterThanZero(value.Value);

        return new PDecimal(value.Value);
    }
}
