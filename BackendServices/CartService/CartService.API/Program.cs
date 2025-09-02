using CartService.Application.HttpClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddHttpClient("HttpClient",client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["CatalogService:BaseUrl"]);
//});

builder.Services.AddHttpClient<CatalogServiceClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiAddress:CatalogApi"]);
});


builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
