using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Workers.Server.Data;
using Workers.Server.Model.Interfaces;
using Workers.Server.Model.Services;
using Workers.Server.Models;

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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>
               (options =>
               {
                   options.User.RequireUniqueEmail = true;
               }).AddEntityFrameworkStores<WorkersDbContext>();


builder.Services.AddTransient<IIndustrialWorker, IndustrialWorkerService>();
builder.Services.AddTransient<IWorkListing, WorkListingService>();
builder.Services.AddTransient<IWorkshop, WorkshopService>();
builder.Services.AddTransient<IReview, ReviewService>();
builder.Services.AddTransient<IUser,IdentityUserService>();


// JWT auth Setup 
builder.Services.AddScoped<JWTTokenService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = JWTTokenService.GetValidationParamerts(builder.Configuration);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Create", policy => policy.RequireClaim("Permissions", "Create"));
    options.AddPolicy("Read", policy => policy.RequireClaim("Permissions", "Read"));
    options.AddPolicy("Update", policy => policy.RequireClaim("Permissions", "Update"));
    options.AddPolicy("Delete", policy => policy.RequireClaim("Permissions", "Delete"));
}); 

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
