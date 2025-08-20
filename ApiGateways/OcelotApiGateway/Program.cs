using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("ocelot-dev.json");
}
else
{
    builder.Configuration.AddJsonFile("ocelot.json");
}

// Add services to the container.
builder.Services.AddOcelot();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

// ocelot middleware
app.UseOcelot().Wait();

app.Run();
