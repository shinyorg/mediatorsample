namespace PeopleModule.Services;

public interface IDataService
{
    Task Delete(int personId, CancellationToken cancellationToken);
    Task<PersonResult?> GetById(int personId, CancellationToken cancellationToken);
    Task<IReadOnlyList<PersonResult>> GetPeople(int[] personIds, CancellationToken cancellationToken);
}