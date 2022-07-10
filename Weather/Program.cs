using Weather.Helpers;
using Weather.Interfaces;
using Weather.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWeatherService, WeatherService>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration =
        $"{Environment.GetEnvironmentVariable("REDIS_HOST")}:{Environment.GetEnvironmentVariable("REDIS_PORT")}";
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.ConfigureHandler(app.Logger, app.Environment.IsDevelopment());
app.UseAuthorization();
app.MapControllers();

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyOrigin();
    policyBuilder.AllowAnyHeader();
    policyBuilder.AllowAnyMethod();
});

app.Run();