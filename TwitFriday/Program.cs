
using Microsoft.EntityFrameworkCore;
using Twitter.Dal.Contexts;
using Twitter.Business;
using Microsoft.AspNetCore.Identity;
using Twitter.Core.Entities;
using Twitter.Business.ExternalServices;
using Microsoft.AspNetCore.Authentication.OAuth;
using Twitter.Business.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
namespace TwitFriday
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<TwitterContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
            });
            //AddIdentity
            builder.Services.AddUserIdentity();
            //Repositories Topic
            builder.Services.AddRepositories();
            //Services Topic
            builder.Services.AddServices();
            //AddBusinessLayer Topic
            builder.Services.AddBusinessLayer();
            //Repositories Blog
            builder.Services.AddBlogRepositories();
            //Services Blog
            builder.Services.AddBlogServices();
            //AddBusinessLayer Blog
            builder.Services.AddBlogBusinessLayer();
            //AddEmail
            builder.Services.AddEmail();

           
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration.GetSection("Jwt")["Issuer"],
                    ValidAudience = builder.Configuration.GetSection("Jwt")["Audience"],
                    IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt")["Key"])),
                    LifetimeValidator = (nb,exp,token,_)=>token!= null ? exp>= DateTime.UtcNow && nb <= DateTime.UtcNow : false,
                };
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                });
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            PathConstants.RootPath = builder.Environment.WebRootPath;
            app.Run();
        }
    }
}
