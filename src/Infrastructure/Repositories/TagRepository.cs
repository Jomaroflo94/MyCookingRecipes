using Domain.Shared;
using Domain.Tags;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class TagRepository(AppWriteDbContext dbContext) : ITagRepository
{
    public Task<Tag?> GetByNameAsync(Text Name, CancellationToken cancellationToken = default)
    {
        return dbContext.Tags.FirstOrDefaultAsync(u => u.Name == Name, cancellationToken);
    }

    public void Insert(Tag tag)
    {
        dbContext.Tags.Add(tag);
    }
}
