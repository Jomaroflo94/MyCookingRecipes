namespace Application.Shared.Get;
public record EntityResponse
{
    public Ulid Id { get; set; }
    public string Name { get; set; }
}
