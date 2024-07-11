# End-To-End Shiny Mediator Sample

This shows a lot of good practices when it comes to constructing .NET MAUI applications.  

## Shiny Mediator
* [GitHub](https://github.com/shinyorg/mediator)
* [Documentation](https://shinylib.net/client/mediator/)
* <a href="https://www.nuget.org/packages/Shiny.Mediator" target="_NEWWINDOW"><img src="https://buildstats.info/nuget/Shiny.Mediator"></a>

## Other Libraries
| Name                                                                    | Description                                                             |
|-------------------------------------------------------------------------|-------------------------------------------------------------------------|
| [Prism](https://prismlibrary.com)                                       | The one and only .NET MVVM Framework we all know and love               |
| [Reactive](https://reactiveui.net)                                      | Reactive Extensions for Apps                                            |
| [Shiny Configuration](https://shinylib.net/client/other/configuration/) | Mobile specific use-case library for Microsoft.Extensions.Configuration |
| [SQLite](https://todo)                                                  | The original SQLite object relational mapper                            |
| [Community Toolkit MVVM](https://github.com/shinyorg/framework)         | modern, fast, and modular MVVM library|
| [Settings View](https://todo)                                           | A beautiful looking table type control for forms                        |
| [.NET MAUI Community Toolkit](https://todo)                             | The premier toolkit for .NET MAUI                                       |

## Reasons for this Engineering

1. Allows cross functional teams to work enable cross functional requirements without being tightly coupled
2. Allows each feature/module to register its own services, controls, pages, viewmodels, etc in isolation while still having access to shared functionality like logging, configuration, etc
3. Through use of Shiny.Mediator.Prism, we can achieve strongly typed routing and navigation parameters via a simple Mediator request that implements the IPrismNavigationRequest

## FAQ

> Does it offer any advantages for smaller apps? 

It can.  In this example, it shows how to make some navigation strongly typed for Prism.
You can layer in middleware easily with Shiny Mediator.  In this sample, even though it is mostly useless,
the use of the cache is shown on the GetListRequestHandler's

> Would I do this on a project without a large amount of team members

There are advantages to using a mediator pattern in your apps.  Just like using one on the server, it allows you 
to break up your app as it grows over time.  Where your request handlers live from the contracts, does not matter.

> Why do you pass the Prism navigation service in the requests?

It allows you to pass the CURRENT contextual navigation service from your viewmodel.  We do have a singleton global navigation
services in Shiny Framework, but we wanted to show a safe bet on how this navigation model can work.

> Did you need to use the cache for a SQLite query

Honestly, no - and you probably never should for a SQLite query.  The point to note here though is that the IDataService may be calling out to a remote
service.  It is also a demo to show off some of what Shiny Mediator can do.  Could that service do the caching?  Absolutely.  Should it do the caching 
at the service layer?  That's up to you to decide!

> Would you really do these cross model data linking request (ie. GetPeopleByVehicleRequest, GetVehiclesByPersonRequest) - Doesn't that cause N+1 and a lot of extra chatter?

Likely not because an app is generally single process.  This shows a common pattern (and often common pitfall in server side microserving).  Pick your fights &
don't overengineer.  Well this does 

## TODO
* Documentation on layouts
  * Contracts are the cross communicators
  * Events are post reactions
  * Request handlers are logic processors
  * Shared loops everything into the Module/Feature libraries and ultimately up to the app
* If navigating in a loop, if I delete the top level "detail", underneath should pop as I return
