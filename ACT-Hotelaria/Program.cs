using System.Text.Json.Serialization;
using ACT_Hotelaria.Application.DI;
using ACT_Hotelaria.Redis.DI;
using ACT_Hotelaria.Redis.Settings;
using ACT_Hotelaria.SqlServer.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    }); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRedisInfrastructure(builder.Configuration);
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.Configure<Settings>(
    builder.Configuration.GetSection("CacheSettings"));
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
