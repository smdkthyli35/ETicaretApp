using ETicaretApp.Application.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUser(CreateUser model);
    }
}
