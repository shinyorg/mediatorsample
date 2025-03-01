namespace PeopleModule.Contracts;

public record DeletePersonCommand(int PersonId) : ICommand;
