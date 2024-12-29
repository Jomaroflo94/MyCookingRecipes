using Primitives.Guards;

namespace Domain.Shared;
public sealed record PInt
{
    public PInt(int value) => Value = value;

    public int Value { get; }
    public static PInt Create(int? value)
    {
        Ensure.NotNull(value);
        Ensure.GreaterThanZero(value.Value);

        return new PInt(value.Value);
    }
}
