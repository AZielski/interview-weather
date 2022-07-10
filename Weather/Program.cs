using Weather.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var redisAddress = Environment.GetEnvironmentVariable("REDIS_HOST");
var redisPort = Environment.GetEnvironmentVariable("REDIS_PORT");

builder.Services.AddStackExchangeRedisCache(options => { options.Configuration = $"{redisAddress}:{redisPort}"; });
var app = builder.Build();

Console.WriteLine($"{redisAddress}:{redisPort}");

app.UseSwagger();
app.UseSwaggerUI();

app.ConfigureHandler(app.Logger);
app.UseAuthorization();
app.MapControllers();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
});

app.Run();