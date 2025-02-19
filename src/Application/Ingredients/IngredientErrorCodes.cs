﻿namespace Application.Ingredients;
public static class IngredientErrorCodes
{
    public static class CreateIngredient
    {
        public const string MissingName = nameof(MissingName);
        public const string DuplicateName = nameof(DuplicateName);
        public const string MissingCategories = nameof(MissingCategories);
        public const string Empty = nameof(Empty);
        public const string Null = nameof(Null);
    }
}
