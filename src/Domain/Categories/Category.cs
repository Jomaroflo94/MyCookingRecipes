using Domain.Ingredients;

namespace Domain.Categories;

public sealed class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    #region Relation Properties
    public ICollection<Ingredient> Ingredients { get; set; } = [];
    #endregion
}
