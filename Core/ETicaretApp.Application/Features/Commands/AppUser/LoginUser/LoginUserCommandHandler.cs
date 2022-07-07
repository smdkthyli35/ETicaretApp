using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Abstractions.Token;
using ETicaretApp.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            DTOs.Token token = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 15);
            return new LoginUserSuccessCommandResponse()
            {
                Token = token
            };
        }
    }
}
