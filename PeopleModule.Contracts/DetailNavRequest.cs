using Shiny.Mediator.Prism;

namespace PeopleModule.Contracts;


// NOTE: we do not implement a handler for this, Prism navigation support is handled by Shiny.Mediator.Prism
public record DetailNavRequest(int PersonId) : PrismNavigationRecord(Routes.Detail);