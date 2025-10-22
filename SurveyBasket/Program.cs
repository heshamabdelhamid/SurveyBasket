using SurveyBasket;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencies(builder.Configuration);

// builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//     .AddEntityFrameworkStores<SurveyBasketDbContext>();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

//app.UseCors("AllowAllOrigins");
app.UseCors();

app.UseAuthorization();

// app.MapIdentityApi<ApplicationUser>();

app.MapControllers();

app.Run();