using Mng_shifts.Data;
using Microsoft.EntityFrameworkCore;
using Mng_shifts.Api;
using Mng_shifts.Core;
using Mng_shifts.Core.IRepositories;
using Mng_shifts.Service.Services;
using Mng_shifts.Core.IServices;
using Mng_shifts.Data.Repositories;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var configuration = builder.Configuration;

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("MngConnectionString"),
        x => x.MigrationsAssembly("Mng_shifts.Data")));


builder.Services.AddControllers()
    .AddJsonOptions(x =>
        x.JsonSerializerOptions
//        .ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve);
.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddControllers()
    .AddJsonOptions(opts =>
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddScoped<IShiftExchangeService, ShiftExchangeService>();
builder.Services.AddScoped<IShiftExchangeRepository, ShiftExchangeRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfileDTO), typeof(MappingProfileModels));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()  // אפשר גם להגביל ל־http://localhost:3000 למשל
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


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
app.UseCors();

//app.UseCors("AllowLocalhost5173");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
