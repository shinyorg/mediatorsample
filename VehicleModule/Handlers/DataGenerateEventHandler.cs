using Bogus;
using SharedLib.Contracts;
using VehicleModule.Services.Impl;

namespace VehicleModule.Handlers;


[RegisterHandler]
public class DataGenerateEventHandler(VehicleSqliteConnection data, ILogger<DataGenerateEventHandler> logger) : IEventHandler<DataGenerateEvent>
{
    public async Task Handle(DataGenerateEvent @event, CancellationToken cancellationToken)
    {
        var animals = new Faker<VehicleModel>()
            .RuleFor(x => x.Manufacturer, x => x.Vehicle.Manufacturer())
            .RuleFor(x => x.Model, x => x.Vehicle.Model())
            .Generate(20);
    }
}