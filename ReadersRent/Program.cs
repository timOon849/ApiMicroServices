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


    //// ��������� HttpClient
    //builder.Services.AddHttpClient<BookService>(client =>
    //{
    //    client.BaseAddress = new Uri("https://api.bookservice.com/"); // ������� URL ��� API ����
    //    client.DefaultRequestHeaders.Add("Accept", "application/json"); // ���������, ��� �� ����� �������� ������ � ������� JSON
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
