using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Azure.Data.Tables;
using System.Collections.Generic;
using System.Threading.Tasks;

public class GetAllUsers
{
    private readonly ILogger<GetAllUsers> _logger;

    public GetAllUsers(ILogger<GetAllUsers> logger)
    {
        _logger = logger;
    }

    [Function("GetAllUsers")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "users")] HttpRequestData req,
        [TableInput("Users", Connection = "AzureWebJobsStorage")] TableClient tableClient)
    {
        _logger.LogInformation("GetAllUsers function processed a request.");

        var users = new List<User>();

        try
        {
            // Manually query the table
            await foreach (var user in tableClient.QueryAsync<User>(filter: $"PartitionKey eq 'Users'"))
            {
                users.Add(user);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error querying table: {ex.Message}");
            return req.CreateResponse(HttpStatusCode.InternalServerError);
        }

        var response = req.CreateResponse(HttpStatusCode.OK);
        await response.WriteAsJsonAsync(users);
        return response;
    }
}