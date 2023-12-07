using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
  );
//builder.Services.AddRazorPages();

//builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

string? connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services
    .AddDbContext<WorkersDbContext>
    (option => option.UseSqlServer(connString));

//builder.Services.AddControllers().AddNewtonsoftJson(option =>
//{
//    option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
//});

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
});

builder.Services.AddTransient<IIndustrialWorker, IndustrialWorkerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
