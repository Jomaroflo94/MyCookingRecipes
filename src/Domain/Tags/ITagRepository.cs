using Domain.Shared;

namespace Domain.Tags;

public interface ITagRepository
{
    Task<Tag?> GetByNameAsync(Text Name, 
        CancellationToken cancellationToken = default);
    Task<List<Tag>?> GetByNamesAsync(List<Text> Names, 
        CancellationToken cancellationToken = default);
    void Insert(Tag tag);
    void Insert(List<Tag> tags);
}
