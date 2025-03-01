namespace PeopleModule.Handlers;


[SingletonHandler]
public class GetListRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<PersonResult>>
{
    public async Task<IReadOnlyList<PersonResult>> Handle(GetListRequest request, MediatorContext context, CancellationToken cancellationToken)
    {
        var personIds = request.PersonIds ?? [];
        var people = await data.GetPeople(personIds, cancellationToken);

        return people;
    }
}