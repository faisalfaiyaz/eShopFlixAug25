using eShopFlix.Web.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("HttpClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiGatewayAddress"]);
});

builder.Services.AddScoped<CatalogServiceClient>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("HttpClient");

    return new CatalogServiceClient(httpClient);
});

builder.Services.AddScoped<AuthServiceClient>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("HttpClient");
    return new AuthServiceClient(httpClient);

});

builder.Services.AddScoped<CartServiceClient>(sp =>
{
    var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient("HttpClient");
    return new CartServiceClient(httpClient);
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie( opt =>
    {
        opt.Cookie.Name = "eShopFlixCookie";
        opt.LoginPath = "/account/login";
        opt.AccessDeniedPath = "/account/accessdenied";
        opt.SlidingExpiration = true;
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
