using ROP;

namespace Domain.Shared;

public class PDecimalErrors
{
    public static readonly Error NegativeOrZero = Error.Problem("PositiveDecimal.NegativeOrZero", "Positive decimal is negative or Zero");
}
