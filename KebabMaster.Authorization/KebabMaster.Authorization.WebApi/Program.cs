using System.Text;
using KebabMaster.Authorization.Domain.Entities;
using KebabMaster.Authorization.Domain.Interfaces;
using KebabMaster.Authorization.Infrastructure.Database;
using KebabMaster.Authorization.Infrastructure.Repositories;
using KebabMaster.Authorization.Infrastructure.Settings;
using KebabMaster.Authorization.Interfaces;
using KebabMaster.Authorization.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddTransient<IApplicationLogger, ApplicationLogger>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database"));
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidAudience = builder.Configuration["TokenData:Issuer"],
        ValidIssuer = builder.Configuration["TokenData:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenData:Secret"]))
    };
});
builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

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

void SetDatabase(WebApplication app)
{
    
    // var database = app.Services.GetService<ApplicationDbContext>();
    // database.Database.EnsureCreated();
    //
    // if (!database.Roles.Any())
    // {
    //     database.Roles.Add(new Role("Admin"));
    //     database.Roles.Add(new Role("User"));
    //
    // }
    //
    // database.SaveChanges();
    //
    // if (!database.Users.Any())
    // {
    //     var user = 
    //         User.Create("testmail@mail.com","Gul" ,"Skrain", "Dukat");
    //     
    //     database.Users.
    // }
}