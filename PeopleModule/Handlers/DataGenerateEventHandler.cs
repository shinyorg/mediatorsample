using Bogus;
using Microsoft.Extensions.Configuration;
using PeopleModule.Services.Impl;
using SharedLib.Contracts;

namespace PeopleModule.Handlers;


[SingletonHandler]
public class DataGenerateEventHandler(
    PeopleSqliteConnection data, 
    IConfiguration configuration,
    ILogger<DataGenerateEventHandler> logger
) : IEventHandler<DataGenerateEvent>
{
    public async Task Handle(DataGenerateEvent @event, EventContext<DataGenerateEvent> context, CancellationToken cancellationToken)
    {
        var count = configuration.GetValue<int>("DataGenerate:People");
        
        var people = new Faker<PersonModel>()
            .RuleFor(x => x.FirstName, x => x.Name.FirstName())
            .RuleFor(x => x.LastName, x => x.Name.LastName())
            .Generate(count);

        await data.InsertAllAsync(people);
        logger.LogInformation($"Generated {count} People");
    }
}