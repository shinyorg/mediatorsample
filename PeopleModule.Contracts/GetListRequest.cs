namespace PeopleModule.Contracts;


public record GetListRequest(int[]? PersonIds = null) : IRequest<IReadOnlyList<PersonResult>>;

// [SourceGenerateJsonConverter]
public partial record PersonResult(int Id, string FirstName, string LastName)
{
    public string FullName => $"{FirstName} {LastName}";
}
