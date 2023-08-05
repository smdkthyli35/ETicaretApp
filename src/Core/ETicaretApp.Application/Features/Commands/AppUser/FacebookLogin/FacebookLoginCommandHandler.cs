using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Abstractions.Token;
using ETicaretApp.Application.Dtos;
using ETicaretApp.Application.Dtos.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace ETicaretApp.Application.Features.Commands.AppUser.FacebookLogin
{
    public class FacebookLoginCommandHandler : IRequestHandler<FacebookLoginCommandRequest, FacebookLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public FacebookLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<FacebookLoginCommandResponse> Handle(FacebookLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.FacebookLoginAsync(request.AuthToken);
            return new()
            {
                Token = token,
            };
        }
    }
}
