using ETicaretApp.Application.Repositories;
using ETicaretApp.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence.Repositories
{
    public class FileReadRepository : ReadRepository<Domain.Entities.File>, IFileReadRepository
    {
        public FileReadRepository(ETicaretAppDbContext context) : base(context)
        {
        }
    }
}
