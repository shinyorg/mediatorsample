using VehicleModule.Contracts;

namespace VehicleModule.Handlers;


[RegisterHandler]
public class DetailsNavRequestHandler : IRequestHandler<DetailNavRequest>
{
    public Task Handle(DetailNavRequest request, CancellationToken cancellationToken)
        => request.Navigator.NavigateAsync(Routes.Detail, (nameof(DetailNavRequest), request));
}