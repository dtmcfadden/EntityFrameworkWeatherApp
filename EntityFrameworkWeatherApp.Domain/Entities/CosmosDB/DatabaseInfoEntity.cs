namespace EntityFrameworkWeatherApp.Domain.Entities.CosmosDB;

public abstract class DatabaseInfoEntity : IDatabaseInfoEntity
{
    public DatabaseInfoEntity(string connectionString)
    {
        CosmosClient = GetClient(connectionString);
        DatabaseRef = GetDatabase(CosmosClient, DatabaseName);
    }

    public Database DatabaseRef { get; init; }

    /// <summary>
    ///     Database name
    /// </summary>
    public abstract string DatabaseName { get; }

    ///// <summary>
    /////     Connection String
    ///// </summary>
    //public required string ConnectionString { get; init; }

    /// <summary>
    ///     Cosmos Client
    /// </summary>
    public CosmosClient CosmosClient { get; init; }

    ///// <summary>
    /////     Database initialized
    ///// </summary>
    //public bool Initialized { get; set; } = false;

    ///// <summary>
    /////     List of containers
    ///// </summary>
    //public List<IContainerInfoEntity> ContainerInfo { get; set; } = [];

    //public CosmosClient GetClient()
    //{
    //    if (CosmosClient is null)
    //        CreateClient();

    //    return CosmosClient!;
    //}

    private static CosmosClient GetClient(string connectionString)
    {
        if (connectionString is null)
            throw new CosmosDBConnectionStringNotFoundException("Unable to create client.");

        return new CosmosClient(
            connectionString: connectionString,
            new CosmosClientOptions()
            {
                AllowBulkExecution = true,
            }
        );
    }

    private static Database GetDatabase(CosmosClient cosmosClient, string databaseName)
    {
        return cosmosClient.GetDatabase(databaseName);
    }

    //public async Task<Database> InitializeDatabase()
    //{
    //    if (Initialized == false)
    //    {
    //        var client = GetClient();
    //        try
    //        {
    //            var result = await client.CreateDatabaseIfNotExistsAsync(DatabaseName);

    //            if (result.StatusCode == HttpStatusCode.OK
    //                || result.StatusCode == HttpStatusCode.Created)
    //            {
    //                Initialized = true;

    //                DatabaseRef = result.Database;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new CosmosDBUnableToCreateDatabaseException(ex.Message);
    //        }
    //    }
    //    return DatabaseRef;
    //}

    //public void AddReplaceContainerInfo(IContainerInfoEntity containerInfoEntity)
    //{
    //    var match = ContainerInfo.First(x =>
    //    x.ContainerName.Equals(containerInfoEntity.ContainerName, StringComparison.OrdinalIgnoreCase));
    //    if (match is null)
    //    {
    //        ContainerInfo.Add(containerInfoEntity);
    //    }
    //    else
    //    {
    //        match = containerInfoEntity;
    //    }
    //}

    //public async Task<Container> InitializeContainer(string containerName)
    //{
    //    var match = GetContainerInfo(containerName);

    //    return await match.InitializeContainer(DatabaseRef);
    //}

    public Container GetContainer(string containerName)
    {
        return CosmosClient.GetContainer(DatabaseName, containerName);
    }

    //public IContainerInfoEntity GetContainerInfo(string containerName)
    //{
    //    var match = ContainerInfo.First(x =>
    //    x.ContainerName.Equals(containerName, StringComparison.OrdinalIgnoreCase));

    //    return match is null ?
    //        throw new CosmosDBContainerNotFoundException(containerName) :
    //        match;
    //}
}
