namespace PeopleModule.Handlers;


[RegisterHandler]
public class GetPeopleRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<PersonResult>>
{
    public Task<IReadOnlyList<PersonResult>> Handle(GetListRequest request, CancellationToken cancellationToken)
        => data.GetAll();
}