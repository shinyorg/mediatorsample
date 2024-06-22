using SharedLib;

namespace OwnerModule.Handlers;


[RegisterHandler]
public class LinkNavRequestHandler : IRequestHandler<LinkNavRequest>
{
    public async Task Handle(LinkNavRequest request, CancellationToken cancellationToken)
    {
        if (request.VehicleId == null && request.PersonId == null)
            throw new InvalidOperationException("You must set the VehicleId or PersonId");

        await request.Navigator.Navigate(
            nameof(LinkPage),
            request.ToNavParam()
        );
    }
}