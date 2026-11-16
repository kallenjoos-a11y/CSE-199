using MeetDb.Data;
using MeetS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<MeetDbContext>(options => 
    options.UseInMemoryDatabase("MeetDatabase"));

builder.Services.AddScoped<MeetService>();

var app = builder.Build();

app.MapControllers();

app.Run();