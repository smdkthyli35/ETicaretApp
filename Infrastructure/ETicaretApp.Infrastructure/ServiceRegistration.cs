using ETicaretApp.Application.Abstractions.Storage;
using ETicaretApp.Application.Abstractions.Storage.Azure;
using ETicaretApp.Infrastructure.Enums;
using ETicaretApp.Infrastructure.Services;
using ETicaretApp.Infrastructure.Services.Storage;
using ETicaretApp.Infrastructure.Services.Storage.Azure;
using ETicaretApp.Infrastructure.Services.Storage.Local;
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
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        //Bu yöntem pek kullanışlı değil!
        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:

                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
