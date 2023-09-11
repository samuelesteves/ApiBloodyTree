using ApiBloodyTree.Business;
using ApiBloodyTree.Context;
using ApiBloodyTree.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<MembroBusiness>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7218", builder =>
    {
        builder.WithOrigins("http://localhost:7218")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
    options.AddPolicy("AllowLocalhost3000", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:7218", "http://localhost:3000")
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.MapControllers();

app.Run();
