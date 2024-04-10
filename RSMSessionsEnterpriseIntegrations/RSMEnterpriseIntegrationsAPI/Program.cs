using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RSMEnterpriseIntegrationsAPI.Application.Mappers;
using RSMEnterpriseIntegrationsAPI.Application.Services;
using RSMEnterpriseIntegrationsAPI.Application.Validators.Product;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Authorization;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Department;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.Product;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.ProductCategory;
using RSMEnterpriseIntegrationsAPI.Domain.Interfaces.SalesOrderHeader;
using RSMEnterpriseIntegrationsAPI.Infrastructure;
using RSMEnterpriseIntegrationsAPI.Infrastructure.Repositories;
using RSMEnterpriseIntegrationsAPI.Middleware;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var jwtKey = builder.Configuration.GetValue<string>("JwtSettings:Key");
var jwtIssuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer");
var jwtAudience = builder.Configuration.GetValue<string>("JwtSettings:Audience");

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AdvWorksDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        opt => opt.MigrationsAssembly(typeof(AdvWorksDbContext).Assembly.FullName));
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(CreateProudctDtoValidator));

builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddTransient<ISalesOrderHeaderRepository, SalesOrderHeaderRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddTransient<IDepartmentService, DepartmentService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IProductCategoryService, ProductCategoryService>();
builder.Services.AddTransient<ISalesOrderHeaderService, SalesOrderHeaderService>();
builder.Services.AddTransient<IAuthorizationService, AuthorizationService>();

ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en-US", false);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
