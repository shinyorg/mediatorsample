namespace PeopleModule.Services.Impl;

public class DataService(PeopleSqliteConnection conn) : IDataService
{
    public async Task<PersonResult> GetById(int animalId)
    {
        var person = await conn.People.FirstOrDefaultAsync(x => x.Id == animalId);
        return ToResult(person);
    }

    public async Task<IReadOnlyList<PersonResult>> GetAll()
    {
        var list = await conn.People.ToListAsync();
        return list.Select(ToResult).ToList();
    }

    static PersonResult ToResult(PersonModel model) => new(model.Id, model.FirstName, model.LastName);
}