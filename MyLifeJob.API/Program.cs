using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyLifeJob.Business;
using MyLifeJob.Business.ExternalServices.Implements;
using MyLifeJob.Business.ExternalServices.Interfaces;
using MyLifeJob.Business.Profiles;
using MyLifeJob.Business.Services.Implements;
using MyLifeJob.Core.Entity;
using MyLifeJob.DAL.Contexts;
using System.Text;
using MyLifeJob.Business.Constants;
using MyLifeJob.DAL;
using Hangfire;
using Microsoft.Extensions.FileProviders;

namespace MyLifeJob.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            //Cors
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers().AddNewtonsoftJson(opt =>
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            //Hangfire
            var hangfireConnectionSTring = builder.Configuration.GetConnectionString("HangfireString");
            builder.Services.AddHangfire(h =>
            {
                h.UseSqlServerStorage(hangfireConnectionSTring);
                RecurringJob.AddOrUpdate<AdvertismentService>(a => a.CheckStatus(), "0 0 * * *");
                RecurringJob.AddOrUpdate<AdvertismentService>(a => a.ExpiresDeletion(), "0 0 * * *");
            });
            builder.Services.AddHangfireServer();

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

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

            builder.Services.AddAutoMapper(typeof(UserMappingProfiles).Assembly);

            builder.Services.AddService();
            builder.Services.AddRepository();

            builder.Services.AddFluentValidation(res =>
            {
                res.RegisterValidatorsFromAssemblyContaining<AppUserService>();
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(res =>
            {
                res.Password.RequireNonAlphanumeric = false;
                res.SignIn.RequireConfirmedEmail = true;
                res.Lockout.MaxFailedAccessAttempts = 3;
                res.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddSignInManager<SignInManager<AppUser>>()
            .AddDefaultTokenProviders();

            var emailConfig = configuration.GetSection("EmailConfiguration")
                .Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    LifetimeValidator = (_, expires, token, _) => token != null ? DateTime.UtcNow.AddHours(4) < expires : false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigninKey"]))
                };
            });
            builder.Services.AddAuthorization();


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

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "wwwroot/imgs")),
                RequestPath = "/wwwroot/imgs"
            });

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseHangfireDashboard();

            //app.UseCustomExceptionHandler();

            app.MapControllers();

            RootContsant.Root = builder.Environment.WebRootPath;

            app.Run();
        }
    }
}