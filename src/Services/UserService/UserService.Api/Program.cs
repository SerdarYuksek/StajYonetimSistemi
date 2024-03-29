﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using UserService.Api.Context;
using UserService.Api.Model;
using UserService.Api.Service;
using UserService.Api.Services;
using UserService.Api.Services.UserService.Api.Services;
using UserService.Api.ValidationRules;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();

// Identity konfigürasyonu
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    // Þifre politikalarýný yapýlandýrma
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 1;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;
})
.AddEntityFrameworkStores<UserIdentityDbContext>()
.AddDefaultTokenProviders();

// Authentication ve Authorization eklemeleri
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
    o.RequireHttpsMetadata = false;
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSingleton<IConnectionMultiplexer>(a => ConnectionMultiplexer.Connect(new ConfigurationOptions
{
    EndPoints = { $"{builder.Configuration.GetValue<string>("Redis:Host")}:{builder.Configuration.GetValue<int>("Redis:Port")}" }
}));

// Dependency Injection
builder.Services.AddScoped<EMailRepository>();
builder.Services.AddScoped<UserRegisterValidation>();
builder.Services.AddScoped<TokenRepository>();
builder.Services.AddScoped<CrudGenericRepository<AppUser>>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IDatabaseAsync>(_ => _.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

// DbContext konfigürasyonu
builder.Services.AddDbContext<UserIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

// Swagger eklemeleri
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
app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
