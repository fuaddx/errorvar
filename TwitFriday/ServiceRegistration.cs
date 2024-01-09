using Microsoft.AspNetCore.Identity;
using Twitter.Core.Entities;
using Twitter.Dal.Contexts;

namespace TwitFriday
{
    public static class ServiceRegistration
    {
          public static IServiceCollection AddUserIdentity(this IServiceCollection services)
        {
           services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                // Password settings.
                /*options.SignIn.RequireConfirmedEmail = true;*/
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 4;
                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                options.User.RequireUniqueEmail = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<TwitterContext>();
            return services;
        }
    }
}
