using ETicaretApp.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ETicaretAppDbContext>(options => options.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ETicaretAppDb;Trusted_Connection=True;"));
        }
    }
}
