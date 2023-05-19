using ETicaretApp.Application.Repositories;
using ETicaretApp.Domain.Entities;
using ETicaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence.Repositories
{
    public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
    {
        public ProductWriteRepository(ETicaretAppDbContext context) : base(context)
        {
        }
    }
}
