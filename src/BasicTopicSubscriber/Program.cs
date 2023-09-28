using BasicTopicSubscriber.Installers;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddServices()    
        .AddSubscribers()
        .AddClients(configuration);

var app = builder.Build();

await app.StartSubscribersAsync();

app.Run();
