using CORE_Layer.Helper;
using CORE_Layer.Services;
using Data_Access_Layer.ContextDb;
using Data_Access_Layer.Interfaces;
using Data_Access_Layer.Repositories;
using Db_Builder.Models.User_Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// App Context connection string
    builder.Services.AddDbContext<UserContext>(options =>
         options.UseSqlServer(builder.Configuration.GetConnectionString("AppConn")));


    // Generic Repository
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserServices>();
builder.Services.AddAutoMapper(typeof(SecurityProfile));

builder.Services.AddIdentity<AppUser, IdentityRole>()
           .AddEntityFrameworkStores<UserContext>()
           .AddDefaultTokenProviders();
var app = builder.Build();

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
