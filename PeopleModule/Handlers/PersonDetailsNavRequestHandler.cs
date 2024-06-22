namespace PeopleModule.Handlers;


[RegisterHandler]
public class PersonDetailNavRequestHandler : IRequestHandler<DetailNavRequest>
{
    public Task Handle(DetailNavRequest request, CancellationToken cancellationToken)
        => request.Navigator.NavigateAsync(
            Routes.Detail, 
            (nameof(request.PersonId), request.PersonId)
        );
}