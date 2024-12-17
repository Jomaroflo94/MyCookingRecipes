using Domain.Shared;
using Domain.Tags;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class TagRepository(AppWriteDbContext dbContext) : ITagRepository
{
    public Task<Tag?> GetByNameAsync(Text Name, 
        CancellationToken cancellationToken = default)
    {
        return dbContext.Tags
            .FirstOrDefaultAsync(u => u.Name == Name, cancellationToken);
    }

    public async Task<List<Tag>?> GetByNamesAsync(List<Text> Names, 
        CancellationToken cancellationToken = default)
    {
        var values = Names.Select(s => s.Value).ToList();

        return await dbContext.Tags.Where(u => values.Contains(u.Name.Value))
            .ToListAsync(cancellationToken);
    }

    public void Insert(Tag tag)
    {
        dbContext.Tags.Add(tag);
    }

    public void Insert(List<Tag> tags)
    {
        dbContext.Tags.AddRange(tags);
    }
}
