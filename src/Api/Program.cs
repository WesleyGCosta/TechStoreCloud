using Api.Data;
using Api.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<IdentityContext>(
    options => options.UseNpgsql("Host=localhost:5433;Database=TechStore;Username=postgres;Password=12345678").UseSnakeCaseNamingConvention());

builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<IdentityContext>();

builder.Services.AddOpenApi();
builder.Services.AddValidation();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();    
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapProductEndpoints();
//app.MapAuthEndpoints();

app.MapIdentityApi<IdentityUser>();

app.Run();

