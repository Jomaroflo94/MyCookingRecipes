using Domain.Ingredients;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class IngredientRepository(AppWriteDbContext dbContext) : IIngredientRepository
{
    public async Task<bool> IsNameUniqueAsync(Text name, CancellationToken cancellationToken = default)
    {
        return !await dbContext.Ingredients.AnyAsync(u => u.Name == name, cancellationToken);
    }

    public void Insert(Ingredient ingredient)
    {
        dbContext.Ingredients.Add(ingredient);
    }
}
