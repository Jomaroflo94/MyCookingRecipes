namespace API.IntegrationTests.Abstractions;
public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
{
    public BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        HttpClient = factory.CreateClient();
    }

    protected HttpClient HttpClient { get; init; }
}
