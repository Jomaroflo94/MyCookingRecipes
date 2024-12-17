using Application.Ingredients.Get;
using Domain.Ingredients;
using Infrastructure.Data;
using Mediator.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using ROP;

namespace Infrastructure.Queries.Ingredients;

internal sealed class GetIngredientsQueryHandler(AppReadDbContext dbContext)
    : IQueryHandler<GetIngredientsQuery, IEnumerable<IngredientResponse>>
{

    public async Task<Result<IEnumerable<IngredientResponse>>> Handle(
        GetIngredientsQuery query,
        CancellationToken cancellationToken)
    {
        List<IngredientResponse>? ingredients = await dbContext.Ingredients
            .Select(u => new IngredientResponse
            {
                Id = u.Id,
                Name = u.Name,
                Quantity = u.Quantity
            }).ToListAsync(cancellationToken);

        if (ingredients.Count == 0)
        {
            return Result.Failure<IEnumerable<IngredientResponse>>(
                IngredientErrors.NoneFound());
        }

        return ingredients;
    }
}
