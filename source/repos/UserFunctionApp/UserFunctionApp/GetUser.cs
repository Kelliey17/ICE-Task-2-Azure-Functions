using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Threading.Tasks;

public class GetUser
{
    private readonly ILogger<GetUser> _logger;

    public GetUser(ILogger<GetUser> logger)
    {
        _logger = logger;
    }

    [Function("GetUser")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users/{id}")] HttpRequestData req,
        string id,
        [TableInput("Users", Connection = "AzureWebJobsStorage")] TableClient tableClient)
    {
        _logger.LogInformation($"GetUser function processed a request for id={id}");

        try
        {
            var response = await tableClient.GetEntityAsync<User>("Users", id);
            var httpResponse = req.CreateResponse(HttpStatusCode.OK);
            await httpResponse.WriteAsJsonAsync(response.Value);
            return httpResponse;
        }
        catch (Azure.RequestFailedException)
        {
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}