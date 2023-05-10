using ESP.Context;
using ESP.Models;
using ESP.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("SqlServerConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));



// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("espcors", options =>
    {
        options.AllowAnyOrigin();
        options.AllowAnyMethod();
        options.AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




app.UseCors("espcors");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
