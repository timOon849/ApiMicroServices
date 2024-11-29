using ReadersAndRent.Interfaces;
using ReadersAndRent.Service;
using Microsoft.EntityFrameworkCore;
using ReadersAndRent.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
var app = builder.Build();
builder.Services.AddScoped<IReaderService, ReaderService>();
builder.Services.AddScoped<IHistoryService, HistoryService>();
builder.Services.AddDbContext<DBCon>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("TestDbString1")), ServiceLifetime.Scoped);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowApiMicroServices", policy =>
    {
        policy.WithOrigins("http://localhost:5172")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
