using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestarauntMenu.Bll;
using RestarauntMenu.Dal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews(configure => { configure.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDishService, DishService>();

builder.Services.AddDbContext<RestarauntContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
