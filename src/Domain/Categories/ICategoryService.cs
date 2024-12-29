using ROP;

namespace Domain.Categories;
public interface ICategoryService
{
    Task<Result<List<Category>>> ValidateAndGetCategories(
        List<Ulid> categories, CancellationToken cancellationToken);
}
