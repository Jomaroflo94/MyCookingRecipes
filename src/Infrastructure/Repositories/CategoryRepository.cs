using Domain.Categories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CategoryRepository(AppWriteDbContext dbContext) : ICategoryRepository
{
    public async Task<List<Category>?> GetByIdsAsync(List<Ulid> ids,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.Where(u => ids.Contains(u.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Category>?> GetExistingIdsAsync(List<Ulid> ids,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories
            .Where(c => ids.Contains(c.Id))
            .ToListAsync(cancellationToken);
    }
}
