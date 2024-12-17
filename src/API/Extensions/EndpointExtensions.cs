using MediatR;
using ROP;
using ROP.Extensions;

namespace API.Extensions;

internal static class EndpointExtensions
{
    internal static async Task<IResult> Send(
        this ISender sender,
        IRequest<Result> command,
        Func<IResult> onSuccess,
        CancellationToken cancellationToken)
    {
        return (await sender.Send(command, cancellationToken))
            .Match(onSuccess, CustomResults.Problem);
    }

    internal static async Task<IResult> Send<T>(
        this ISender sender, 
        IRequest<Result<T>> command,
        Func<IResult> onSuccess,
        CancellationToken cancellationToken)
    {
        return (await sender.Send(command, cancellationToken))
            .Match(onSuccess, CustomResults.Problem);
    }

    internal static async Task<IResult> Send<T>(
        this ISender sender,
        IRequest<Result<T>> command,
        Func<T, IResult> onSuccess,
        CancellationToken cancellationToken)
    {
        return (await sender.Send(command, cancellationToken))
            .Match(onSuccess, CustomResults.Problem);
    }
}
