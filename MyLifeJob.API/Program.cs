using Microsoft.EntityFrameworkCore;
using MyLifeJob.Business.Profiles;
using MyLifeJob.DAL.Contexts;
using MyLifeJob.Business;
using MyLifeJob.Business.Services.Implements;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using MyLifeJob.Core.Entity;
using MyLifeJob.API.Helpers;

namespace MyLifeJob.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddAutoMapper(typeof(UserMappingProfiles).Assembly);

            builder.Services.AddService();

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

            app.UseCustomExceptionHandler();

            app.MapControllers();

            app.Run();
        }
    }
}