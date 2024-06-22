namespace PeopleModule.Handlers;

[RegisterHandler]
public class ListNavRequestHandler : IRequestHandler<ListNavRequest>
{
    public Task Handle(ListNavRequest request, CancellationToken cancellationToken)
        => request.Navigator.NavigateAsync(Routes.List);
}