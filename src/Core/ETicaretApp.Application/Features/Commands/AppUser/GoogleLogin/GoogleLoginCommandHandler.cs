using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Dtos;
using MediatR;

namespace ETicaretApp.Application.Features.Commands.AppUser.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public GoogleLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token = await _authService.GoogleLoginAsync(request.IdToken, 15);
            return new()
            {
                Token = token,
            };
        }
    }
}
