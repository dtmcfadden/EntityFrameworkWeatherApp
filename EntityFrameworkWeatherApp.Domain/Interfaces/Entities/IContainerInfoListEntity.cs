using EntityFrameworkWeatherApp.Domain.Entities.CosmosDB;

namespace EntityFrameworkWeatherApp.Domain.Interfaces.Entities;
public interface IContainerInfoListEntity
{
    public Dictionary<string, ContainerInfoEntity> ContainerList { get; set; }
}
