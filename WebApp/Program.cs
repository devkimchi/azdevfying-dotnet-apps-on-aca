using WebApp.Clients;
using WebApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<IApiAppClient, ApiAppClient>((sp, client) =>
{
#if DEBUG
    client.BaseAddress = new Uri("https://localhost:5051");
#elif RELEASE
    var config = sp.GetService<IConfiguration>();
    client.BaseAddress = new Uri(config!["API_ENDPOINT_URL"]!);
#endif
});

builder.Services.AddHttpClient<IFuncAppClient, FuncAppClient>((sp, client) =>
{
#if DEBUG
    client.BaseAddress = new Uri("http://localhost:7071");
#elif RELEASE
    var config = sp.GetService<IConfiguration>();
    client.BaseAddress = new Uri(config!["FUNC_ENDPOINT_URL"]!);
#endif
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
