using ROP;

namespace Domain.Categories;
public static class CategoryErrors
{
    public static Error NotExist(IEnumerable<Ulid> ids) => Error.Conflict(
        "Categories.NotExist", $"The following category identifiers do not exist: {string.Join(',', ids)}");

    public static readonly Error Empty = Error.Validation(
        "Categories.Empty", "No category specified for some Ingredient");
}
