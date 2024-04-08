using Microsoft.EntityFrameworkCore;
using WeatherAPI.CosmosDB.Entities;

namespace WeatherAPI.Repositories;

// https://medium.com/@kevinwilliams.dev/ef-core-cosmos-db-3da250b47d6c
// https://learn.microsoft.com/en-us/azure/cosmos-db/hierarchical-partition-keys?tabs=net-v3%2Cbicep
// https://medium.com/@kevinwilliams.dev/ef-core-cosmos-db-3da250b47d6c
// https://github.com/dotnet/EntityFramework.Docs/blob/main/samples/core/Cosmos/ModelBuilding/OrderContext.cs
// https://learn.microsoft.com/en-us/ef/core/providers/cosmos/?tabs=dotnet-core-cli


public class WeatherDBContext : DbContext
{
    public string DbName { get; } = "weather";

    public WeatherDBContext(DbContextOptions<WeatherDBContext> options) :
        base(options)
    {

    }

    public DbSet<WeatherRequestHistoryEntity> WeatherRequestHistoryEntity { get; set; }

    [Time]
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultContainer(DbName);

        builder.Entity<WeatherRequestHistoryEntity>()
            .ToContainer("WeatherRequestHistory")
            .HasPartitionKey(x => x.Id)
            .HasNoDiscriminator();
    }
}
