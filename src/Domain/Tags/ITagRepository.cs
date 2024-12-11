using Domain.Shared;

namespace Domain.Tags;

public interface ITagRepository
{
    Task<Tag?> GetByNameAsync(Text Name, CancellationToken cancellationToken = default);
    void Insert(Tag tag);
}
