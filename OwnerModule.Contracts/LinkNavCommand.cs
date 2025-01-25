using Shiny.Mediator.Prism;

namespace OwnerModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record LinkNavCommand(int? PersonId, int? VehicleId) : PrismNavigationRecord("LinkPage");