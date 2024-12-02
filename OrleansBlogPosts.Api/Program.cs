using OrleansBlogPosts.Api.Models;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddKeyedRedisClient("orleans-redis");
builder.AddKeyedRedisClient("grain-storage");
builder.UseOrleans();

// Add services to the container.
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<BlogPost>());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.Run();
