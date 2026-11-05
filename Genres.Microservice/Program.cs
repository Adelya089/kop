
using Genres.Microservice;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GenreDatabase>();

var app = builder.Build();

var database = app.Services.GetRequiredService<GenreDatabase>();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

app.Run();
