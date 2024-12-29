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

internal sealed class GetIngredientByIdQueryHandler(AppReadDbContext dbContext) 
    : IQueryHandler<GetIngredientByIdQuery, IngredientResponse>
{

    public async Task<Result<IngredientResponse>> Handle(
        GetIngredientByIdQuery query, 
        CancellationToken cancellationToken)
    {
        return Result.Create(await dbContext.Ingredients
                    .Include(i => i.Categories)
                    .FirstOrDefaultAsync(u => u.Id == query.IngredientId, 
                        cancellationToken), 
                IngredientErrors.NotFound(query.IngredientId))
            .Map(SetResponse);
    }

    private static IngredientResponse SetResponse(
        IngredientRead ingredient)
    {
        return new IngredientResponse
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            Categories = ingredient.Categories.Select(s => new EntityResponse
            {
                Id = s.Id,
                Name = s.Name
            })
        };
    }
}
