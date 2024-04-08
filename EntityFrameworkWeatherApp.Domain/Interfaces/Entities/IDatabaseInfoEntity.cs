namespace EntityFrameworkWeatherApp.Domain.Interfaces.Entities;

public interface IDatabaseInfoEntity
{
    /// <summary>
    ///     Database reference
    /// </summary>
    public Database DatabaseRef { get; init; }

    /// <summary>
    ///     Database name
    /// </summary>
    public abstract string DatabaseName { get; }

    /// <summary>
    ///     Cosmos Client
    /// </summary>
    public CosmosClient CosmosClient { get; init; }

    ///// <summary>
    /////     Connection String
    ///// </summary>
    //public string ConnectionString { get; init; }

    ///// <summary>
    /////     Gets Cosmost Client
    ///// </summary>
    //public CosmosClient GetClient();

    ///// <summary>
    /////     List of Database Containers
    ///// </summary>
    //public List<IContainerInfoEntity> ContainerInfo { get; set; }

    /////// <summary>
    ///////     Database initialized
    /////// </summary>
    ////public bool Initialized { get; set; }

    /////// <summary>
    ///////     Initialize Database
    /////// </summary>
    ////public Task<Database> InitializeDatabase();

    /////// <summary>
    ///////     Initialize Container
    /////// </summary>
    ////public Task<Container> InitializeContainer(string containerName);

    /// <summary>
    ///     Container reference
    /// </summary>
    public Container GetContainer(string containerName);

    ///// <summary>
    /////     Container info
    ///// </summary>
    //public IContainerInfoEntity GetContainerInfo(string containerName);
}
