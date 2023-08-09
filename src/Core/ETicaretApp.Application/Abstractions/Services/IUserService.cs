using ETicaretApp.Application.Dtos.User;
using ETicaretApp.Domain.Entities.Identity;
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
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate);
    }
}
