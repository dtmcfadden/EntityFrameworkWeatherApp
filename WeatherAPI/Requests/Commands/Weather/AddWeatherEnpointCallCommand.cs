using EntityFrameworkWeatherApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WeatherAPI.CosmosDB.Entities;
using WeatherAPI.CosmosDB.IRepository;
using WeatherAPI.CosmosDB.UnitOfWork;
using WeatherAPI.Repositories;

namespace WeatherAPI.Requests.Commands.Weather;

[Time]
public sealed record AddWeatherEnpointCallCommand(HttpTrackingEntity HttpTracking, string? SentParams = null) : IRequest<Result<EntityEntry<WeatherRequestHistoryEntity>>>;

[Time]
public sealed class AddWeatherEnpointCallHandler(
    IWeatherRequestHistoryRepository weatherRequestHistoryRepository,
    IUnitOfWork<WeatherDBContext> unitOfWork
    ) :
    IRequestHandler<AddWeatherEnpointCallCommand, Result<EntityEntry<WeatherRequestHistoryEntity>>>
{
    private readonly IWeatherRequestHistoryRepository _weatherRequestHistoryRepository = weatherRequestHistoryRepository;
    private readonly IUnitOfWork<WeatherDBContext> _unitOfWork = unitOfWork;

    public async Task<Result<EntityEntry<WeatherRequestHistoryEntity>>> Handle(
        AddWeatherEnpointCallCommand request,
        CancellationToken cancellationToken)
    {
        var weatherRequestHistoryEntity = new WeatherRequestHistoryEntity(
            request.HttpTracking, request.SentParams)
        {
            Id = Guid.NewGuid(),
        };

        var entityEntry = await _weatherRequestHistoryRepository.AddAsync(
            weatherRequestHistoryEntity, cancellationToken);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Result<EntityEntry<WeatherRequestHistoryEntity>>(entityEntry);
    }
}