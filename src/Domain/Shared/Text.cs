using Primitives.Guards;

namespace Domain.Shared;

public sealed record Text
{
    public Text(string value) => Value = value;

    public string Value { get; }

    public static Text Create(string? text)
    {
        Ensure.NotNullOrEmpty(text);

        return new Text(text);
    }
}
