using ApplicationCore;
using Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration);
var app = builder.Build();
await app.Services.InitializeDatabasesAsync();

app.UseInfraestructure();
app.Run();
