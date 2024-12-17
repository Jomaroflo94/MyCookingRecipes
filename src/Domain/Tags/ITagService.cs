using Domain.Shared;

namespace Domain.Tags;
public interface ITagService
{
    Task<Guid> GetTagAsync(Text Name,
        CancellationToken cancellationToken);

    Task<IEnumerable<(Guid Id, Text Name)>> GetTagsAsync(
        List<Text> Names,
        CancellationToken cancellationToken);

    Guid CreateTag(Text Name);

    IEnumerable<(Guid Id, Text Name)> CreateTags(
        List<Text> Names);
}
