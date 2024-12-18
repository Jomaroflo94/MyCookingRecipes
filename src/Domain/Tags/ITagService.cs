using Domain.Shared;

namespace Domain.Tags;
public interface ITagService
{
    Task<Ulid> GetTagAsync(Text Name,
        CancellationToken cancellationToken);

    Task<IEnumerable<(Ulid Id, Text Name)>> GetTagsAsync(
        List<Text> Names,
        CancellationToken cancellationToken);

    Ulid CreateTag(Text Name);

    IEnumerable<(Ulid Id, Text Name)> CreateTags(
        List<Text> Names);
}
