namespace PeopleModule.Handlers;


[RegisterHandler]
public class GetListRequestHandler(IDataService data) : IRequestHandler<GetListRequest, IReadOnlyList<PersonResult>>
{
    public Task<IReadOnlyList<PersonResult>> Handle(GetListRequest request, CancellationToken cancellationToken)
        => data.GetAll();
}