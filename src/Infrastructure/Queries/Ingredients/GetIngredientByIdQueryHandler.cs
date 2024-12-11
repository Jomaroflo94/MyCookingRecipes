using Application.Ingredients.Get;
using Domain.Ingredients;
using Infrastructure.Data;
using Mediator.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Infrastructure.Queries.Ingredients;

internal sealed class GetIngredientByIdQueryHandler(AppReadDbContext dbContext) 
    : IQueryHandler<GetIngredientByIdQuery, IngredientResponse>
{

    public async Task<Result<IngredientResponse>> Handle(
        GetIngredientByIdQuery query, 
        CancellationToken cancellationToken)
    {
        IngredientResponse? ingredient = await dbContext.Ingredients
            .Where(u => u.Id == query.IngredientId)
            .Select(u => new IngredientResponse
            {
                Id = u.Id,
                Name = u.Name,
                Quantity = u.Quantity
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (ingredient is null)
        {
            return Result.Failure<IngredientResponse>(
                IngredientErrors.NotFound(query.IngredientId));
        }

        return ingredient;
    }
}
