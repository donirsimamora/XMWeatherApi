using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XMWeatherApi.Data;
using XMWeatherApi.Services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//disable for now
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(policy =>
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader());
//});
// Seed data as singleton in-memory store
builder.Services.AddSingleton<SeedData>();
// Register weather implementations. Use Mock by default
builder.Services.AddHttpClient("OpenWeather", client => {
    client.BaseAddress = new Uri("https://api.openweathermap.org/");
});

// To use the OpenWeather provider, comment out below code MockWeatherService  
builder.Services.AddScoped<IWeatherService, MockWeatherService>();

// To use OpenWeatherService from https://api.openweathermap.org/ uncomment below code 
//builder.Services.AddScoped<IWeatherService, OpenWeatherService>();

var app = builder.Build();
//use swagger to test API is can be acccessed
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
// Enable static files (wwwroot)
app.UseDefaultFiles();
app.UseStaticFiles();
//disable for now
//app.UseCors();
app.MapControllers();
app.Run();
