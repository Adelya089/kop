using Books.Microservice.Data;
using QuestPDF.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<BookDatabase>();
builder.Services.AddHttpClient("genres", client => client.BaseAddress = new Uri(
    builder.Configuration["GenresServiceUrl"] ?? "http://genres:8080/"));
QuestPDF.Settings.License = LicenseType.Community;
var app = builder.Build();
await app.Services.GetRequiredService<BookDatabase>().InitializeAsync();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();
