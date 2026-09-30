using Azure;
using Azure.Data.Tables;

public class User : ITableEntity
{
    public string PartitionKey { get; set; } = "Users";
    public string RowKey { get; set; }        // the user ID
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}