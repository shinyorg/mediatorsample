namespace PeopleModule.Services.Impl;

// sqlite pcl net does not use cancellation tokens but it is good practice to put these in the abstraction
public class DataService(PeopleSqliteConnection conn) : IDataService
{
    public Task Delete(int personId, CancellationToken _)
        => conn.People.DeleteAsync(x => x.Id == personId);

    public async Task<PersonResult> GetById(int personId, CancellationToken _)
    {
        var person = await conn.People.FirstOrDefaultAsync(x => x.Id == personId);
        return ToResult(person);
    }

    public async Task<IReadOnlyList<PersonResult>> GetPeople(int[] personIds, CancellationToken _)
    {
        if (personIds.Length == 0)
        {
            var list1 = await conn
                .People
                .OrderBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToListAsync();
            
            return list1.Select(ToResult).ToList();
        }
        
        var list = await conn
            .People
            .Where(x => personIds.Contains(x.Id))
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync();

        return list.Select(ToResult).ToList();
    }

    static PersonResult ToResult(PersonModel model) => new(model.Id, model.FirstName, model.LastName);
}