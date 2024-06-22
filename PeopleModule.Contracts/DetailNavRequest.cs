namespace PeopleModule.Contracts;

public record DetailNavRequest(INavigationService Navigator, int PersonId) : IRequest;