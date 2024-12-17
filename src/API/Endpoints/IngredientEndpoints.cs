using API.Extensions;
using Application.Ingredients.Create;
using Application.Ingredients.Get;
using MediatR;

namespace API.Endpoints;

internal static class IngredientEndpoints
{
    internal static void MapIngredientEndpoints(
        this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup("/api/ingredients")
            .WithTags("Ingredients");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetIngredientsQuery();

            return await sender.Send(query, Results.Ok, cancellationToken);
        });

        group.MapGet("/{ingredientId}", async (Guid ingredientId, ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var query = new GetIngredientByIdQuery(ingredientId);

            return await sender.Send(query, Results.Ok, cancellationToken);
        });

        group.MapPost("/", async (CreateIngredientRequest request, ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new CreateIngredientCommand(
                request.Name,
                request.Quantity);

            return await sender.Send(command, Results.Created, cancellationToken);
        });

        group.MapPost("/list", async (List<CreateIngredientRequest> request, ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new CreateIngredientsCommand(request);

            return await sender.Send(command, Results.Created, cancellationToken);
        });
    }
}
