var builder = WebApplication.CreateBuilder(args);
// .UseServiceProviderFactory(new AutofacServiceProviderFactory())
builder.Services.AddShinyMediator();
//builder.Services.AddDiscoveredMediatorHandlersFromShinyApp();

var app = builder.Build();

//app.UseShinyMediatorEndpointHandlers(builder.Services);
app.UseHttpsRedirection();
app.Run();