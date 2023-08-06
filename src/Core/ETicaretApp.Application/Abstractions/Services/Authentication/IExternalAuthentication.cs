using ETicaretApp.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Abstractions.Services.Authentication
{
    public interface IExternalAuthentication
    {
        Task<Dtos.Token> GoogleLoginAsync(string idToken, int accessTokenLifeTime);
        Task<Dtos.Token> FacebookLoginAsync(string authToken, int accessTokenLifeTime);
    }
}
