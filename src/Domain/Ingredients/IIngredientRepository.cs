using Domain.Shared;

namespace Domain.Ingredients;

public interface IIngredientRepository
{
    Task<bool> IsNameUniqueAsync(Text name, CancellationToken cancellationToken = default);
    void Insert(Ingredient ingredient);
}
