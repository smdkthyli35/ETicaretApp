﻿using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Abstractions.Services.Configurations;
using ETicaretApp.Application.Abstractions.Storage;
using ETicaretApp.Application.Abstractions.Token;
using ETicaretApp.Infrastructure.Enums;
using ETicaretApp.Infrastructure.Services;
using ETicaretApp.Infrastructure.Services.Configurations;
using ETicaretApp.Infrastructure.Services.Storage;
using ETicaretApp.Infrastructure.Services.Storage.Azure;
using ETicaretApp.Infrastructure.Services.Storage.Local;
using ETicaretApp.Infrastructure.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlTypes;

namespace ETicaretApp.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastractureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IApplicationService, ApplicationService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
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
                    services.AddScoped<IStorage, AzureStorage>();
                    break;
                case StorageType.AWS:
                    break;
                default:
                    break;
            }
        }
    }
}
