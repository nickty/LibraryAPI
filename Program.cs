using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();  // This ensures routing is enabled.

app.UseAuthorization();  // Optional if you have authorization enabled.
app.MapControllers();    // This maps the controllers to the routes.

app.Run();
