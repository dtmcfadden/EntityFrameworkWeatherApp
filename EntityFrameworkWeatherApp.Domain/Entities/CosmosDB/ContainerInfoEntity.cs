namespace EntityFrameworkWeatherApp.Domain.Entities.CosmosDB;

public class ContainerInfoEntity : IContainerInfoEntity
{
    ///// <summary>
    /////     Container Name
    ///// </summary>
    //public required Container ContainerRef { get; set; }

    /// <summary>
    ///     Container Name
    /// </summary>
    public required string ContainerName { get; set; }

    /// <summary>
    ///     Container partition Key
    /// </summary>
    public required string PartitionKeyPath { get; set; }

    ///// <summary>
    /////     Container is initialized
    ///// </summary>
    //public bool IsInitialized { get; set; } = false;

    //public async Task<Container> InitializeContainer(Database database)
    //{
    //    if (IsInitialized == false)
    //    {
    //        ContainerRef = await database.CreateContainerIfNotExistsAsync(
    //            id: ContainerName,
    //            partitionKeyPath: PartitionKeyPath
    //        );
    //    }
    //    return ContainerRef;
    //}
}
