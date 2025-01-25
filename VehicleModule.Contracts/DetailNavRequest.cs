using Shiny.Mediator.Prism;

namespace VehicleModule.Contracts;

// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record DetailNavRequest(int VehicleId) : PrismNavigationRecord(Routes.Detail);