using ROP;
using ROP.Extensions;

namespace Domain.Shared;

public sealed record Text
{
    public Text(string value) => Value = value;

    public string Value { get; }

    public static Result<Text> Create(string? text)
    {
        return Result.Create(text)
            .Ensure(value => !string.IsNullOrEmpty(text), TextErrors.Empty)
            .Map(value => new Text(text ?? string.Empty));
    }
}
