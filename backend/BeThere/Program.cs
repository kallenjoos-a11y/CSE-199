using MeetCG.Services;
using MeetDb.Data;
using MeetS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//TODO change from in memory database to sqlLite

builder.Services.AddDbContext<MeetDbContext>(options => 
    options.UseSqlite(
        builder.Configuration.GetConnectionString("MeetDatabase")));

builder.Services.AddScoped<MeetService>();

builder.Services.AddSingleton<MeetCodeGenerator>();

var app = builder.Build();

app.MapControllers();

app.Run();