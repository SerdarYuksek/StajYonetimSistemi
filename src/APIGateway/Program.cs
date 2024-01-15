using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
               .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();
builder.Services.AddOcelot();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware kayýt sýrasý önemli
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Ocelot'u kullan
app.UseOcelot().Wait();

app.MapControllers();
app.Run();