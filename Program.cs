using System;
using LibraryAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with connection string
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDB")));
    

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();


// using (var scope = app.Services.CreateScope()) 
// {
//     var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>(); 
//     bool isConnected = dbContext.TestConnection(); 
//     if (!isConnected) 
//     { 
//         Console.WriteLine("Failed to connect to the database."); 
//         // Handle the failure appropriately (e.g., exit the application, log the error, etc.) 
//     } 
//     else 
//     { 
//         Console.WriteLine("Successfully connected to the database."); 
//     } 
// }

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
