﻿using ETicaretApp.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretApp.Application.Features.Commands.Role.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommandRequest, DeleteRoleCommandResponse>
    {
        private readonly IRoleService _roleService;

        public DeleteRoleCommandHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<DeleteRoleCommandResponse> Handle(DeleteRoleCommandRequest request, CancellationToken cancellationToken)
        {
            bool result = await _roleService.DeleteRole(request.Id);
            return new() { Succeeded = result };
        }
    }
}
