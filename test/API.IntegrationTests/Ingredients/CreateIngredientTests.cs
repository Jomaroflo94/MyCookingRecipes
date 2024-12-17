using System.Net;
using System.Net.Http.Json;
using API.IntegrationTests.Abstractions;
using API.IntegrationTests.Shared;
using Application.Ingredients;
using Application.Ingredients.Create;
using FluentAssertions;

namespace API.IntegrationTests.Ingredients;
public class CreateIngredientTests : BaseIntegrationTest
{
    public CreateIngredientTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenNameIsEmpty()
    {
        // Arrange
        var request = new CreateIngredientRequest("", 1000);

        // Act
        HttpResponseMessage response = await HttpClient
            .PostAsJsonAsync("api/ingredient", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        ProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                IngredientErrorCodes.CreateIngredient.MissingName
            ]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task Should_ReturnBadRequest_WhenQuantityIsNegativeOrZero(decimal quantity)
    {
        // Arrange
        var request = new CreateIngredientRequest("Ingredient", quantity);

        // Act
        HttpResponseMessage response = await HttpClient
            .PostAsJsonAsync("api/ingredient", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        ProblemDetails problemDetails = await response.GetProblemDetails();

        problemDetails.Errors.Select(e => e.Code)
            .Should()
            .Contain([
                IngredientErrorCodes.CreateIngredient.NegativeOrZeroQuantity
            ]);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var request = new CreateIngredientRequest("Ingredient", 1);

        // Act
        HttpResponseMessage response = await HttpClient
            .PostAsJsonAsync("api/ingredient", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_ReturnConflict_WhenIngredientExists()
    {
        // Arrange
        var request = new CreateIngredientRequest("Ingredient", 1);

        // Act
        await HttpClient.PostAsJsonAsync("api/ingredient", request);

        HttpResponseMessage response = await HttpClient.PostAsJsonAsync("api/ingredient", request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}
