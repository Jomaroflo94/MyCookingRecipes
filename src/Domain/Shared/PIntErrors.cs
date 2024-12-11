using ROP;

namespace Domain.Shared;

public class PIntErrors
{
    public static readonly Error NegativeOrZero = Error.Problem("PositiveInt.NegativeOrZero", "Positive int is negative or zero");
}
