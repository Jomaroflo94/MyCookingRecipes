using ROP;
using ROP.Extensions;

namespace Domain.Categories;
public sealed class CategoryService(ICategoryRepository categoryRepository)
    : ICategoryService
{
    public async Task<Result<List<Category>>> ValidateAndGetCategories(
        List<Ulid> categories, 
        CancellationToken cancellationToken)
    {
        return await Result.Create(categories)
            .Ensure(e => e.Count != 0,
                CategoryErrors.Empty).ToAsync()
            .Bind(GetCategories, cancellationToken)
            .Ensure(e => e.Count == categories.Count,
                GetNotExistingIds(categories));
    }

    #region Functions Properties

    private readonly Func<List<Ulid>, CancellationToken, Task<Result<List<Category>>>>
        GetCategories = async (categories, cancellationToken) =>
        {
            //TODO: Si existen en cache, se obtienen de ahi directamente, sino
            //se buscan y despues se almacenan en cache
            int x = 0;

            return await categoryRepository
                .GetExistingIdsAsync(categories, cancellationToken);
        };

    #endregion

    #region Private Methods

    private static Func<List<Category>, Error> GetNotExistingIds(
        List<Ulid> categories)
    {
        return (existingCategories) =>
        {
            IEnumerable<Ulid> missingIds = categories
                .Except(existingCategories!.Select(s => s.Id));

            return CategoryErrors.NotExist(missingIds);
        };
    }

    #endregion
}
