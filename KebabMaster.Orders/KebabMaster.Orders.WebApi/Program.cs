using System.Text;
using KebabMaster.Orders.Domain;
using KebabMaster.Orders.Domain.Entities;
using KebabMaster.Orders.Domain.Interfaces;
using KebabMaster.Orders.Domain.Services;
using KebabMaster.Orders.Infrastructure.Database;
using KebabMaster.Orders.Infrastructure.Logger;
using KebabMaster.Orders.Infrastructure.Repositories;
using KebabMaster.Orders.Infrastructure.Settings;
using KebabMaster.Orders.Interfaces;
using KebabMaster.Orders.Mappings;
using KebabMaster.Orders.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserManagementService, UserManagementService>();
builder.Services.AddTransient<IOrderApiService, OrderApiService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IMenuRepository, MenuRepository>();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IApplicationLogger, ApplicationLogger>();

builder.Services.Configure<DatabaseOptions>(
    builder.Configuration.GetSection("Database"));

builder.Services.AddAutoMapper(typeof(OrderProfile));
builder.Services.AddAutoMapper(conf =>
{
    conf.AddProfile<UserProfile>();
    conf.AddProfile<OrderProfile>();
    
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddCors();

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

}
app.UseSwagger();
app.UseSwaggerUI();
SetupDatabase();

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void SetupDatabase()
{
    DatabaseOptions settings = builder.Configuration.GetSection("Database").Get<DatabaseOptions>();
    var database = new ApplicationDbContext(Options.Create(settings));
    database.Database.EnsureCreated();

    if (!database.MenuItems.Any())
    {

        database.MenuItems.Add(new MenuItem(new Guid("8b0ab337-3e17-4be3-9e41-bd9b96681ef4"),"Chicken Kebab", 9.99));
        database.MenuItems.Add(new MenuItem(new Guid("c4e43f62-17a4-4b97-9326-2cd662a95509"),"Beef Kebab", 10.99));
        database.MenuItems.Add(new MenuItem(new Guid("40554097-9c86-43a3-8bce-8526e1b30501"),"Lamb Kebab", 12.99));
        database.MenuItems.Add(new MenuItem(new Guid("ea55f719-c495-4d64-af24-22d608dad6cc"),"Vegetable Kebab", 8.99));
        database.MenuItems.Add(new MenuItem(new Guid("6ed522a4-9d68-4c59-99ff-232a5e931287"),"Mixed Kebab", 11.99));
    }

    database.SaveChanges();
    
    if (!database.Roles.Any())
    {
        database.Roles.Add(new Role("Admin"));
        database.Roles.Add(new Role("User"));
    
    }
    
    database.SaveChanges();
    
    if (!database.Users.Any())
    {
        var adminUser = 
            User.Create("sgdukat@hotmail.com","Gul" ,"Skrain", "Dukat");
    
        adminUser.PaswordHash = "799DBF90EE52688EB50516DE263415C05207AD00866860331795784F1EC950CF";
        adminUser.Roles = new List<Role>() { database.Roles.First(r => r.Name == "Admin") };
        database.Users.Add(adminUser);
    }

    database.SaveChanges();
}