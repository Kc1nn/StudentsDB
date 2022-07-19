using Microsoft.EntityFrameworkCore;
using StudentsDB.Models;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = studentsdb;Trusted_Connection=true";

builder.Services.AddDbContext<StudentsContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.MapDefaultControllerRoute();

app.Run();