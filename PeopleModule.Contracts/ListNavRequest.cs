namespace PeopleModule.Contracts;

public record ListNavRequest(INavigationService Navigator) : IRequest;
