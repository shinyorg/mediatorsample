namespace PeopleModule.Contracts;

public record DeletePersonEvent(int PersonId, string FirstName, string LastName) : IEvent;
