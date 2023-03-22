using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Backend.TechChallenge.Infrastructure.Interfaces.Models;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add Database
builder.Services.AddDbContext<TechCallengeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();

// Add Swagguer
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Apply database migration
var autoMigrateDB = builder.Configuration.GetValue<Boolean>("ConnectionStrings:AutoMigrate");
if (autoMigrateDB != null && autoMigrateDB)
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<TechCallengeDbContext>();
        db.Database.Migrate();
    }
}

app.Run();
