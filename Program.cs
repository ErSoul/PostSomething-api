using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using PostSomething_api.Database;
using PostSomething_api.Models;
using PostSomething_api.Services.Implementation;
using PostSomething_api.Services.Interface;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
#pragma warning disable CS8604 // Possible null reference argument.

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if(builder.Environment.IsDevelopment() 
    && builder.Configuration.GetConnectionString("InMemoryConnection") != string.Empty 
    && builder.Configuration.GetConnectionString("InMemoryConnection") is not null
)
    builder.Services.AddDbContext<ApplicationContext>(
        options => options
                    .UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryConnection"))
                    .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
else
    builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddIdentityCore<ApiUser>(options =>
{
    options.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<PostSomething_api.Database.ApplicationContext>().AddDefaultTokenProviders();
builder.Services.AddTransient<IUserManager<ApiUser>, UserManager>();

builder.Services
    .AddHttpContextAccessor()
    .AddAuthorization()
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, PostSomething_api.Middlewares.AuthorizationMiddleware>();

builder.Services.AddCors(options =>
{
    if(builder.Environment.IsDevelopment())
        options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
    else
    {
        //-- TODO: add specific origins for staging and production
    }
});

#pragma warning restore CS8604 // Possible null reference argument.

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
