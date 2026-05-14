using FaqService.Api.OpenApi;
using FaqService.Api.Seed;
using FaqService.Application.Services;
using FaqService.Domain.Interfaces;
using FaqService.Infrastructure.Contexts;
using FaqService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// custom OpenApi schema and document transformers
builder.Services.AddFaqOpenApi();

builder.Services.AddDbContext<FaqDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<FaqManager>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.Title = "LMS FAQ API";
        options.Theme = ScalarTheme.Laserwave;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// seeds initial data, in a real application this would be done via migrations or a separate seeding process
await app.SeedDataAsync();

app.Run();
