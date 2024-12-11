using System.Net;
using System.Net.Http.Json;
using API.IntegrationTests.Abstractions;
using Application.Ingredients.Create;
using Application.Ingredients.Get;
using FluentAssertions;

namespace API.IntegrationTests.Ingredients;
public class GetIngredientTests : BaseIntegrationTest
{
    public GetIngredientTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenIngredientDoesNotExist()
    {
        // Arrange
        var ingredientId = Guid.NewGuid();

        // Act
        HttpResponseMessage response = await HttpClient.GetAsync($"api/ingredients/{ingredientId}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnIngredient_WhenIngredientExists()
    {
        // Arrange
        Guid ingredientId = await CreateIngredientAsync();

        // Act
        IngredientResponse? ingredient = await HttpClient
            .GetFromJsonAsync<IngredientResponse>($"api/ingredients/{ingredientId}");

        // Assert
        ingredient.Should().NotBeNull();
    }

    private async Task<Guid> CreateIngredientAsync()
    {
        var request = new CreateIngredientRequest("Ingredient", 1);

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/ingredients", request);

        return await response.Content.ReadFromJsonAsync<Guid>();
    }
}
