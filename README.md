# XMWeatherApi
XM Weather Application
How to run
1. Ensure .NET 9 SDK is installed.
   Ensure Swashbuckle is installed dotnet add package Swashbuckle.AspNetCore
   Ensure Moq is installed dotnet add package Moq
   dotnet xunit is installed add package xunit
   run before test the unit test dotnet build
2. cd src/XMWeatherApi/XMWeatherApi
3. dotnet restore

4. dotnet run — API will run on https://localhost:7129 (or port printed on command prompt).
5. add static file serving to the API project ( app.UseStaticFiles() and place index.html files in wwwroot ).
6. for xunit use test explorer to run each test methods
To switch to OpenWeatherMap

Add your API key to appsettings.json 
OpenWeather__ApiKey . , put your ApiKey in there.
Register OpenWeatherService in Program.cs instead of MockWeatherService .

Notes, 

Dew point: OpenWeather's free current-weather endpoint does not provide dew point directly; 
For production compute dew point from temp & humidity using a standard formula. 
For the mock
Service we return a mocked dew point; extend OpenWeatherService to compute a true dew
point.
Consider caching upstream weather responses with IMemoryCache to avoid hitting external API
limits.
Frontend is intentionally minimal: no framework, plain JS fetch calls.
