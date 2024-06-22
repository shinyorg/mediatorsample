namespace PeopleModule.Services;

public interface IDataService
{
    Task<PersonResult> GetById(int personId);
    Task<IReadOnlyList<PersonResult>> GetAll();
}