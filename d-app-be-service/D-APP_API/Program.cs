
using D_APP_API;
using D_APP_API.Middlewares;
using D_APP_Core.Interfaces.IRepositories;
using D_APP_Core.Interfaces.IServices;
using D_APP_Core.Services;
using D_APP_Infrastructure.Repositories;
using D_APP_Infrastructure.Repositories.SqlServer;
using D_APP_Infrastructure.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Đọc cấu hình từ appsettings.json
var jwtSettings = builder.Configuration.GetSection("Jwt");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("*")
                          .AllowAnyHeader()
                           .AllowAnyMethod();
                      });
});
// Thêm dịch vụ xác thực JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
        };

        // Cấu hình để Swagger có thể sử dụng JWT token từ Authorization header mà không cần tiền tố 'Bearer '
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Lấy token từ query string nếu có
                if (context.Request.Query.ContainsKey("access_token"))
                {
                    context.Token = context.Request.Query["access_token"];
                }
                return Task.CompletedTask;
            }
        };
    });
//

builder.Services.AddControllers();

//config odata
builder.Services.AddControllers().AddOData(option => option.Select().Filter().Count().OrderBy().Expand().SetMaxTop(100));

builder.Services.AddSwaggerGen();

//config Repositories

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IUserService, UserService>();

//config DB
builder.Services.AddDbContext<DAppContext>(option =>
{
    var cnt = builder.Configuration.GetConnectionString("DBConnection");
    option.UseSqlServer(cnt);
});//program

//config middleware
builder.Services.AddExceptionHandler<GlobalExceptionHandlingMiddleware>();
builder.Services.AddProblemDetails();
//builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();



// Cấu hình Swagger để hỗ trợ xác thực JWT
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "DAPP API for THUYHTHE163763",
        Description = "Đây là API cho DAPP! { \"username\": \"your_username\",  \"password\": \"Ho@ng123\"}",
        Contact = new OpenApiContact
        {
            Name = "Facebook",
            Url = new Uri("https://www.facebook.com/userHTXP/")
        },
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(option =>
    {
        
        //option.RoutePrefix = "docs";
        option.SwaggerEndpoint("/swagger/v1/swagger.json", "My test api v1");
    });
}


app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler(opt => { });

app.MapControllers();

app.Run();
