using API.Extensions;
using Application.Ingredients.Create;
using Application.Ingredients.Get;
using MediatR;
using ROP;
using ROP.Extensions;

namespace API.Endpoints;

internal static class IngredientEndpoints
{
    internal static void MapIngredientEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("api/ingredients/{ingredientId}", 
            async (Guid ingredientId, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetIngredientByIdQuery(ingredientId);

            Result<IngredientResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        }).WithTags("Ingredients");

        builder.MapPost("api/ingredients", 
            async (CreateIngredientRequest request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateIngredientCommand(
                request.Name,
                request.Quantity);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Created, CustomResults.Problem);
        }).WithTags("Ingredients");
    }
}
