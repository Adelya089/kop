
using Genres.Microservice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GenreDatabase>();

var app = builder.Build();

var database = app.Services.GetRequiredService<GenreDatabase>();

for (int attempt = 1; attempt <= 10; attempt++)
{
    try
    {
        await database.InitializeAsync();
        break;
    }
    catch (Exception) when (attempt < 10)
    {
        await Task.Delay(3000);
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
