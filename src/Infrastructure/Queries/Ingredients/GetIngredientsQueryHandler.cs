using Application.Ingredients.Get;
using Application.Shared.Get;
using Domain.Ingredients;
using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Mediator.Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using ROP;
using ROP.Extensions;

namespace Infrastructure.Queries.Ingredients;

internal sealed class GetIngredientsQueryHandler(AppReadDbContext dbContext)
    : IQueryHandler<GetIngredientsQuery, IEnumerable<IngredientResponse>>
{

    public async Task<Result<IEnumerable<IngredientResponse>>> Handle(
        GetIngredientsQuery query,
        CancellationToken cancellationToken)
    {
        return Result.Create(await dbContext.Ingredients
                .Include(i => i.Categories)
                .ToListAsync(cancellationToken))
            .Ensure(e => e.Count > 0, IngredientErrors.NoneFound)
            .Map(SetResponse);
    }

    private static IEnumerable<IngredientResponse> SetResponse(
        List<IngredientRead> ingredients)
    {
        return ingredients.Select(u => new IngredientResponse
        {
            Id = u.Id,
            Name = u.Name,
            Categories = u.Categories.Select(s => new EntityResponse
            {
                Id = s.Id,
                Name = s.Name
            })
        });
    }
}
