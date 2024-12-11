using System.Net.Http.Json;
using Primitives;
using ROP;

namespace API.IntegrationTests.Shared;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<ProblemDetails> GetProblemDetails(
        this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response");
        }

        ProblemDetails problemDetails = await response
            .Content
            .ReadFromJsonAsync<ProblemDetails>();

        Ensure.NotNull(problemDetails);

        return problemDetails;
    }
}


internal class ProblemDetails
{
    public string Type { get; set; }

    public string Title { get; set; }

    public int Status { get; set; }

    public string Detail { get; set; }

    public List<Error> Errors { get; set; }
}
