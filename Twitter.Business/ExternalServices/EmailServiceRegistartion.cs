using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Business.ExternalServices.Implements;
using Twitter.Business.ExternalServices.Interfaces;
using Twitter.Business.Repositories.Implements;
using Twitter.Business.Repositories.Interfaces;

namespace Twitter.Business.ExternalServices
{
    public static class EmailServiceRegistartion
    {
        public static IServiceCollection AddEmail(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
        
    }
}
