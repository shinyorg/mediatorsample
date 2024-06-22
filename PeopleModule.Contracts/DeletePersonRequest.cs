namespace PeopleModule.Contracts;

public record DeletePersonRequest(int PersonId) : IRequest;
