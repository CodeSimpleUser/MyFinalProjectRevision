using Autofac;
using Autofac.Core;
using Autofac.Diagnostics;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.EntityFramework;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstact;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
//builder.Services.AddScoped<IProductService, ProductManager>();
//var ContainerBuilder = new ContainerBuilder();
//ContainerBuilder.RegisterType<EfProductDal>().As<IProductDal>();
//ContainerBuilder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
//ContainerBuilder.RegisterType<EfUserDal>().As<IUserDal>();
//ContainerBuilder.RegisterType<ProductManager>().As<IProductService>();
//ContainerBuilder.RegisterType<CategoryManager>().As<ICategoryService>();
//ContainerBuilder.RegisterType<JwtHelper>().As<ITokenHelper>();
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//ContainerBuilder.RegisterType<UserManager>().As<IUserService>();
//var container = ContainerBuilder.Build();

//var tracer = new DefaultDiagnosticTracer();
//tracer.OperationCompleted += (sender, args) =>
//{
//    Trace.WriteLine(args.TraceContent);
//};
//container.SubscribeToDiagnostics(tracer);

//using var scope = container.BeginLifetimeScope();
//scope.Resolve<IProductService>();
//builder.Services.AddSingleton<IProductService, ProductManager>();
//builder.Services.AddSingleton<ICategoryService, CategoryManager>();
//builder.Services.AddSingleton<IUserService, UserManager>();
//builder.Services.AddSingleton<ITokenHelper, JwtHelper>();


builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = tokenOptions.Audience,
        ValidIssuer = tokenOptions.Issuer,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
    };
});
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Origin",
        builder => builder.WithOrigins("http://localhost:4200/").AllowAnyMethod().AllowAnyOrigin());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseStaticFiles(); 
app.UseRouting();

app.UseCors("Origin");

app.UseAuthorization();
app.MapControllers();

app.Run();
