using Microsoft.EntityFrameworkCore;
using NotificationSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<UserContext>(options =>
    options.UseInMemoryDatabase("UserDb"));

var app = builder.Build();

app.MapControllers();

app.Run();
