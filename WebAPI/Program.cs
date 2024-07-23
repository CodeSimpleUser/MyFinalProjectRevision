using Autofac;
using Autofac.Core;
using Autofac.Diagnostics;
using Autofac.Extensions.DependencyInjection;
using Azure.Core;
using Business.Abstract;
using Business.Concrete;
using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using Core.Extensions;
using Core.Utilities.IoC;


//using Core.Utilities.Security.Cookies;
using Core.Utilities.Security.Encryption;
using Core.Utilities.Security.JWT;
using DataAccess.Abstact;
using DataAccess.Concrete.EntitiyFramework;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.Diagnostics;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
/*
builder.Services.AddScoped<IProductService, ProductManager>();
var ContainerBuilder = new ContainerBuilder();
ContainerBuilder.RegisterType<EfProductDal>().As<IProductDal>();
ContainerBuilder.RegisterType<EfCategoryDal>().As<ICategoryDal>();
ContainerBuilder.RegisterType<EfUserDal>().As<IUserDal>();
ContainerBuilder.RegisterType<ProductManager>().As<IProductService>();
ContainerBuilder.RegisterType<CategoryManager>().As<ICategoryService>();
//ContainerBuilder.RegisterType<JwtHelper>().As<ITokenHelper>(); */

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
/*
ContainerBuilder.RegisterType<UserManager>().As<IUserService>();
var container = ContainerBuilder.Build();

var tracer = new DefaultDiagnosticTracer();
tracer.OperationCompleted += (sender, args) =>
{
    Trace.WriteLine(args.TraceContent);
};
container.SubscribeToDiagnostics(tracer);

using var scope = container.BeginLifetimeScope();
scope.Resolve<IProductService>();
builder.Services.AddSingleton<IProductService, ProductManager>();
builder.Services.AddSingleton<ICategoryService, CategoryManager>();
builder.Services.AddSingleton<IUserService, UserManager>();
builder.Services.AddSingleton<ITokenHelper, JwtHelper>();
*/

builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddScoped<IAuthService, AuthManager>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
ServiceTool.Create(builder.Services);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddHttpContextAccessor();


//For token
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidAudience = tokenOptions.Audience,
//        ValidIssuer = tokenOptions.Issuer,
//        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
//    };
//});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = "UserLoginCookie";
        options.Events.OnSignedIn = (context) =>
        {
            context.Properties.AllowRefresh = true;

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        };
        options.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;

    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder
        .SetIsOriginAllowed(x => true)
               .AllowAnyMethod()
                .AllowAnyHeader()
        .AllowCredentials());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors();

app.UseCookiePolicy(
    new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.Always,

    });

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultControllerRoute();

app.Run();
