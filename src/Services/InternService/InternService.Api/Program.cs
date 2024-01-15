using InternService.Api.Context;
using InternService.Api.Model;
using InternService.Api.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InternDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddSingleton<IConnectionMultiplexer>(a => ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { $"{builder.Configuration.GetValue<string>("Redis:Host")}:{builder.Configuration.GetValue<int>("Redis:Port")}" }
}));

builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IDatabaseAsync>(_ => _.GetRequiredService<IConnectionMultiplexer>().GetDatabase());
builder.Services.AddScoped<CrudGenericRepository<InternInfo>>();
builder.Services.AddScoped<CrudGenericRepository<InternStatus>>();

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
