using Bogus;
using Microsoft.Extensions.Configuration;
using SharedLib.Contracts;
using VehicleModule.Services.Impl;

namespace VehicleModule.Handlers;


[SingletonHandler]
public class DataGenerateEventHandler(
    VehicleSqliteConnection data, 
    IConfiguration configuration,
    ILogger<DataGenerateEventHandler> logger
) : IEventHandler<DataGenerateEvent>
{
    public async Task Handle(DataGenerateEvent @event, EventContext<DataGenerateEvent> context, CancellationToken cancellationToken)
    {
        var count = configuration.GetValue<int>("DataGenerate:Vehicles");
        var vehicles = new Faker<VehicleModel>()
            .RuleFor(x => x.Manufacturer, x => x.Vehicle.Manufacturer())
            .RuleFor(x => x.Model, x => x.Vehicle.Model())
            .Generate(count);

        await data.InsertAllAsync(vehicles);
        logger.LogInformation($"Generated {count} Vehicles");
    }
}