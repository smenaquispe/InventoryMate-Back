var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

var services = builder.Services;
startup.ConfigureServices(services);

var app = builder.Build();
startup.Configure(app, app.Environment);

app.Run();
