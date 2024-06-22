# End-To-End Shiny Mediator Sample

This shows a lot of good practices when it comes to constructing .NET MAUI applications.  

## TODO
* Add Owner to a vehicle
* Adding an owning, triggers an event to update data
* Cache

## Other Libraries
| Name                                                                    | Description                                                             |
|-------------------------------------------------------------------------|-------------------------------------------------------------------------|
| [Prism](https://prismlibrary.com)                                       | The one and only .NET MVVM Framework we all know and love               |
| [Reactive](https://reactiveui.net)                                      | Reactive Extensions for Apps                                            |
| [Shiny Configuration](https://shinylib.net/client/other/configuration/) | Mobile specific use-case library for Microsoft.Extensions.Configuration |
| [SQLite](https://todo)                                                  | The original SQLite object relational mapper                            |
| [Shiny Framework](https://github.com/shinyorg/framework)                | Brings RXUI, Prism, & Shiny together with some extra toys               |
| [Settings View](https://todo)                                           | A beautiful looking table type control for forms                        |
| [.NET MAUI Community Toolkit](https://todo)                             | The premier toolkit for .NET MAUI                                       |

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