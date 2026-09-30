# CE Task 2 - HTTP-triggered Azure Functions (.NET 8 Isolated)

An Azure Functions project demonstrating HTTP triggers and Azure Table Storage integration.

## Functions
1. `CreateUser` (POST /api/users) - Accepts a JSON user, saves it to Azure Table Storage.
2. `GetUser` (GET /api/users/{id}) - Retrieves a specific user by ID.
3. `GetAllUsers` (GET /api/users) - Retrieves all users in the table.

## Attributions and References
- **Azure Functions .NET Isolated Worker Guide** – Microsoft Docs. Used for understanding the `Program.cs` setup, `HttpRequestData`, and isolated worker bindings.
- **Azure Tables bindings for Azure Functions** – Microsoft Docs. Used as reference for `[TableInput]` and `[TableOutput]` attributes and `TableClient` bindings.
- **Azure.Data.Tables NuGet Package** – Used for `ITableEntity` implementation.
- **Postman Testing** – Microsoft Docs. Used for guidance on testing HTTP-triggered Azure Functions locally.
- **Stack Overflow** – Resolved the known issue regarding the isolated worker `TableInput` binding returning a 500 error when binding to `IEnumerable<T>`. Workaround implemented using `TableClient.QueryAsync`.
