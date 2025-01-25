var builder = WebApplication.CreateBuilder(args);

builder.Services.AddShinyMediator();
//builder.Services.AddDiscoveredMediatorHandlersFromShinyApp();
var app = builder.Build();

//app.UseShinyMediatorEndpointHandlers(builder.Services);
app.UseHttpsRedirection();

app.Run();
