using FaqService.Application.Services;
using FaqService.Domain.Interfaces;
using FaqService.Infrastructure.Contexts;
using FaqService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<FaqDbContext>(o =>
o.UseInMemoryDatabase("FaqDb"));

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

app.Run();
