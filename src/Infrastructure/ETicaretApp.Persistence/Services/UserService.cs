﻿using ETicaretApp.Application.Abstractions.Services;
using ETicaretApp.Application.Dtos.User;
using ETicaretApp.Application.Exceptions;
using ETicaretApp.Application.Features.Commands.AppUser.CreateUser;
using ETicaretApp.Application.Helpers;
using ETicaretApp.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserResponse> CreateUser(CreateUser model)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                NameSurname = model.NameSurname,
                UserName = model.Username,
                Email = model.Email,
            }, model.Password);

            CreateUserResponse response = new() { Succeeded = result.Succeeded };

            if (result.Succeeded)
                response.Message = "Kullanıcı başarıyla oluşturuldu!";
            else
                foreach (var error in result.Errors)
                {
                    response.Message += $"{error.Code} - {error.Description}\n";
                }

            return response;
        }

        public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
        {
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is not null)
            {
                resetToken = resetToken.UrlDecode();

                IdentityResult result = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
                if (result.Succeeded)
                    await _userManager.UpdateSecurityStampAsync(user);
                else
                    throw new PasswordChangeFailedException();
            }
        }

        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
    }
}
