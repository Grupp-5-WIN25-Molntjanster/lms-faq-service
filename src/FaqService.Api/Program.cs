using FaqService.Api.OpenApi;
using FaqService.Api.Seed;
using FaqService.Application.Services;
using FaqService.Domain.Interfaces;
using FaqService.Infrastructure.Contexts;
using FaqService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using FaqService.Api.Security;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// custom OpenApi schema and document transformers
builder.Services.AddFaqOpenApi();

builder.Services.AddDbContext<FaqDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFaqRepository, FaqRepository>();
builder.Services.AddScoped<FaqManager>();

// allows CORS needed for next.js frontend to call the API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
        // TODO: in production, restrict this to the actual frontend domain
        //.WithOrigins("https://ditt-frontend.vercel.app")
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference("/docs", options =>
    {
        options.Title = "LMS FAQ API";
        options.Theme = ScalarTheme.Laserwave;
        options.DefaultHttpClient = new(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

// may be causing azure to crash
//app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// seeds initial data, in a real application this would be done via migrations or a separate seeding process
try { await app.SeedDataAsync(); }
catch (Exception ex) { app.Logger.LogError(ex, "Seeding failed"); }

app.Run();
