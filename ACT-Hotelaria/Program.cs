using System.Text.Json.Serialization;
using ACT_Hotelaria.Application.DI;
using ACT_Hotelaria.Auth.DI;
using ACT_Hotelaria.DI;
using ACT_Hotelaria.Domain.Model;
using ACT_Hotelaria.Middleware;
using ACT_Hotelaria.Redis.DI;
using ACT_Hotelaria.Redis.Settings;
using ACT_Hotelaria.SqlServer.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddWebApi(builder.Configuration);
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.WriteIndented = true;
    }); builder.Services.AddEndpointsApiExplorer();

builder.Services.AddRedisInfrastructure(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.Configure<Settings>(
        builder.Configuration.GetSection("CacheSettings"));

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<GlobalErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication(); 
app.UseAuthorization();  
app.MapControllers();
app.Run();
