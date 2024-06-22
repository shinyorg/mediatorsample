using Bogus;
using PeopleModule.Services.Impl;
using SharedLib.Contracts;

namespace PeopleModule.Handlers;


public class DataGenerateEventHandler(PeopleSqliteConnection data) : IEventHandler<DataGenerateEvent>
{
    public async Task Handle(DataGenerateEvent @event, CancellationToken cancellationToken)
    {
        var people = new Faker<PersonModel>()
            .RuleFor(x => x.FirstName, x => x.Name.FirstName())
            .RuleFor(x => x.LastName, x => x.Name.LastName())
            .Generate(50);

        await data.InsertAllAsync(people);
    }
}