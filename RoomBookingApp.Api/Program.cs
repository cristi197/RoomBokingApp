using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RoomBookingApp.Core.DataServices;
using RoomBookingApp.Core.Processors;
using RoomBookingApp.Persistence;
using RoomBookingApp.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();

var connString = "DataSource=:memory:";
var conn = new SqliteConnection(connString);
conn.Open();

builder.Services.AddControllers();
builder.Services.AddDbContext<RoomBookingAppDbContext>(option => option.UseSqlite(conn));

var dbBuilder = new DbContextOptionsBuilder<RoomBookingAppDbContext>();
dbBuilder.UseSqlite(conn);
using var context = new RoomBookingAppDbContext(dbBuilder.Options);
context.Database.EnsureCreated();

builder.Services.AddScoped<IRoomBookingService, RoomBookingService>();
builder.Services.AddScoped<IRoomBookingRequestProcessor, RoomBookingRequestProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
