using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReadersRent.Context;
using ReadersRent.Interfaces;
using ReadersRent.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBCon>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString")), ServiceLifetime.Scoped);
builder.Services.AddScoped<IReader, ReaderService>();

var app = builder.Build();


    //// Настройка HttpClient
    //builder.Services.AddHttpClient<BookService>(client =>
    //{
    //    client.BaseAddress = new Uri("https://api.bookservice.com/"); // Базовый URL для API книг
    //    client.DefaultRequestHeaders.Add("Accept", "application/json"); // Указываем, что мы хотим получать данные в формате JSON
    //});



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
