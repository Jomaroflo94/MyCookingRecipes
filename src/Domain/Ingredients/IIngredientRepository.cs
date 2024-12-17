using Domain.Shared;

namespace Domain.Ingredients;

public interface IIngredientRepository
{
    Task<bool> IsNameUniqueAsync(Text Name, CancellationToken cancellationToken = default);
    Task<bool> AreNamesUniquesAsync(List<Text> Names, CancellationToken cancellationToken = default);
    void Insert(Ingredient ingredient);
    void Insert(List<Ingredient> ingredients);
}
