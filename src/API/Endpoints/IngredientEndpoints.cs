using API.Extensions;
using Application.Ingredients.Create;
using Application.Ingredients.Get;
using MediatR;

namespace API.Endpoints;

internal static class IngredientEndpoints
{
    private const string Path = "/api/ingredients";

    internal static void MapIngredientEndpoints(
        this IEndpointRouteBuilder builder)
    {
        RouteGroupBuilder group = builder.MapGroup(Path)
            .WithTags("Ingredients");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetIngredientsQuery();

            return await sender.Send(query, Results.Ok, cancellationToken);
        });

        group.MapGet("/{ingredientId}", async (Ulid ingredientId, ISender sender, 
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
                request.Categories);

            return await sender.Send(command, 
                (result) => Results.Created($"{Path}/{result}", result), 
                cancellationToken);
        });

        group.MapPost("/list", async (List<CreateIngredientRequest> request, ISender sender, 
            CancellationToken cancellationToken) =>
        {
            var command = new CreateIngredientsCommand(request);

            return await sender.Send(command,
                (result) => Results.Created(Path, result),
                cancellationToken);
        });
    }
}
