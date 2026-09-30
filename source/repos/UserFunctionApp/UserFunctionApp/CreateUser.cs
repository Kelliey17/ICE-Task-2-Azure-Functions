using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

public class CreateUser
{
    private readonly ILogger<CreateUser> _logger;

    public CreateUser(ILogger<CreateUser> logger)
    {
        _logger = logger;
    }

    [Function("CreateUser")]
    [TableOutput("Users", Connection = "AzureWebJobsStorage")]
    public User Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "users")] HttpRequestData req,
        [Microsoft.Azure.Functions.Worker.Http.FromBody] User user)
    {
        _logger.LogInformation("CreateUser function processed a request.");

        if (string.IsNullOrEmpty(user.RowKey))
            user.RowKey = Guid.NewGuid().ToString();

        user.PartitionKey = "Users";

        // Returning the entity writes it to Table Storage via the output binding
        return user;
    }
}   