using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection;

namespace EntityFrameworkWeatherApp.Infrastructure.CosmosDb;

public abstract class CosmosDBBaseContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        AddBuilderEntitySettings(modelBuilder);

        RemovePluralizingTableNameConvention(modelBuilder);

        //AddCreatedDatesAndUpdatedDate(modelBuilder);
    }

    private void RemovePluralizingTableNameConvention(ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
        {
            //entity.SetTableName(entity.DisplayName());
        }
    }

    public virtual void AddBuilderEntitySettings(ModelBuilder modelBuilder)
    {

    }

    //private void AddCreatedDatesAndUpdatedDate(ModelBuilder modelBuilder)
    //{
    //    //Debugger.Launch();

    //    var allEntities = modelBuilder.Model.GetEntityTypes()
    //        .Where(x => { return !ExcludeShadowProperty(x); });

    //    foreach (var entity in allEntities)
    //    {
    //        entity.AddProperty("CreatedDate", typeof(DateTimeOffset))
    //            .SetDefaultValueSql("sysdatetimeoffset()");
    //        entity.AddProperty("UpdatedDate", typeof(DateTimeOffset))
    //            .SetDefaultValueSql("sysdatetimeoffset()");
    //    }
    //}

    //private bool ExcludeShadowProperty(IMutableEntityType mutableEntityType)
    //{
    //    if (mutableEntityType.IsKeyless) return true;

    //    var attribute = Attribute.GetCustomAttribute(mutableEntityType.ClrType, typeof(OmitShadowPropertyAttribute));

    //    return attribute != null;
    //}
}
