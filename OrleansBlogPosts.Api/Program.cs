using OrleansBlogPosts.Api.Extensions;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddKeyedRedisClient("orleans-redis");
builder.AddKeyedRedisClient("grain-storage");
builder.UseOrleans(builder => builder.UseDashboard(x => x.HostSelf = true));

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(_ => _.Servers = []);
}

app.UseHttpsRedirection();

app.Map("/dashboard", x => x.UseOrleansDashboard());
app.MapFeatureEndpoints();

app.Run();
