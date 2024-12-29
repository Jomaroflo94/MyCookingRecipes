namespace Domain.Categories;
public interface ICategoryRepository
{
    Task<List<Category>?> GetByIdsAsync(List<Ulid> ids,
        CancellationToken cancellationToken = default);

    Task<List<Category>?> GetExistingIdsAsync(List<Ulid> ids,
        CancellationToken cancellationToken = default);
}
