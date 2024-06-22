using SharedLib;

namespace VehicleModule.Handlers;


[RegisterHandler]
public class DetailsNavRequestHandler : IRequestHandler<DetailNavRequest>
{
    public Task Handle(DetailNavRequest request, CancellationToken cancellationToken)
        => request.Navigator.Navigate(Routes.Detail, request.ToNavParam());
}