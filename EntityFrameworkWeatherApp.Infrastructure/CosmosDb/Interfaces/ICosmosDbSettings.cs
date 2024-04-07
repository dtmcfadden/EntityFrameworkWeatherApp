using EntityFrameworkWeatherApp.Domain.Interfaces.Entities;

namespace EntityFrameworkWeatherApp.Infrastructure.CosmosDb.Interfaces;
public interface ICosmosDbSettings
{
    /// <summary>
    /// Set to true to by default check if database has been initiated.
    /// </summary>
    public bool RunInitiationCheck { get; set; }

    /// <summary>
    /// Connection String Value
    /// </summary>
    public string CosmosConnectionString { get; set; }

    /// <summary>
    ///     Database name
    /// </summary>
    public IDatabaseInfoEntity Database { get; }
}
