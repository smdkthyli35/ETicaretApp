﻿using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Abstractions.Token;
using ETicaretApp.Application.Dtos;
using ETicaretApp.Application.Dtos.Facebook;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager, ITokenHandler tokenHandler)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> FacebookLoginAsync(string authToken)
        {
            string accessTokenResponse = await _httpClient.GetStringAsync(
                $"https://graph.facebook.com/oauth/access_token?client_id={_configuration["ExternalLoginSettings:Facebook:Client_ID"]}" +
                $"&clientSecret={_configuration["ExternalLoginSettings:Facebook:Client_Secret"]}&grant_type=client_credentials");

            FacebookAccessTokenResponse? facebookAccessTokenResponse = JsonSerializer.Deserialize<FacebookAccessTokenResponse>(accessTokenResponse);
            string userAccessTokenValidation = await _httpClient.GetStringAsync($"https://graph.facebook.com/debug_token?input_token={authToken}&access_token={facebookAccessTokenResponse?.AccessToken}");

            FacebookUserAccessTokenValidation? validation = JsonSerializer.Deserialize<FacebookUserAccessTokenValidation>(userAccessTokenValidation);

            if (validation?.Data.IsValid is not null)
            {
                string userInfoResponse = await _httpClient.GetStringAsync($"https://graph.facebook.com/me?fields=email,name&access_token={authToken}");

                FacebookUserInfoResponse? userInfo = JsonSerializer.Deserialize<FacebookUserInfoResponse>(userInfoResponse);

                var info = new UserLoginInfo("FACEBOOK", validation.Data.UserId, "FACEBOOK");
                Domain.Entities.Identity.AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

                bool result = user != null;
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(userInfo?.Email);
                    if (user == null)
                    {
                        user = new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Email = userInfo?.Email,
                            UserName = userInfo?.Email,
                            NameSurname = userInfo?.Name
                        };
                        var identityResult = await _userManager.CreateAsync(user);
                        result = identityResult.Succeeded;
                    }
                }

                if (result)
                {
                    await _userManager.AddLoginAsync(user, info); //AspNetUserLogins

                    Token token = _tokenHandler.CreateAccessToken(5);
                    return token;
                }

            }
            throw new Exception("Invalid external authentication.");
        }

        public Task<Token> GoogleLoginAsync(string idToken)
        {
            throw new NotImplementedException();
        }

        public Task LoginAsync()
        {
            throw new NotImplementedException();
        }
    }
}
