using Mapster;
using MapsterMapper;
using SurveyBasket.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPollService, PollService>();

//Add mapster
//var MappingConfig = TypeAdapterConfig.GlobalSettings;
//MappingConfig.Scan(Assembly.GetExecutingAssembly());
//builder.Services.AddSingleton<IMapper>(new Mapper(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
