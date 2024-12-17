using Domain.Ingredients;
using Domain.Shared;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class IngredientRepository(AppWriteDbContext dbContext) : IIngredientRepository
{
    public async Task<bool> IsNameUniqueAsync(Text Name, CancellationToken cancellationToken = default)
    {
        return !await dbContext.Ingredients.AnyAsync(u => u.Name == Name, cancellationToken);
    }

    public async Task<bool> AreNamesUniquesAsync(List<Text> Names, CancellationToken cancellationToken = default)
    {
        var values = Names.Select(s => s.Value).ToList();

        return !await dbContext.Ingredients.AnyAsync(u => values.Contains(u.Name.Value), cancellationToken);
    }

    public void Insert(Ingredient ingredient)
    {
        dbContext.Ingredients.Add(ingredient);
    }

    public void Insert(List<Ingredient> ingredients)
    {
        dbContext.Ingredients.AddRange(ingredients);
    }
}
