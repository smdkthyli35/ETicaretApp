﻿using ETicaretApp.Application.Abstractions.Storage;
using ETicaretApp.Infrastructure.Enums;
using ETicaretApp.Infrastructure.Services.Storage;
using ETicaretApp.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlTypes;

namespace ETicaretApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfstractureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }

        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:
                    break;
                case StorageType.AWS:
                    break;
                default:
                    break;
            }
        }
    }
}
