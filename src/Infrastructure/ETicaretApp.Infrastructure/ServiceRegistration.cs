using ETicaretApp.Application.Services;
using ETicaretApp.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfstractureServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, FileService>();
        }
    }
}
