using ETicaretApp.Application.Consts;
using ETicaretApp.Application.CustomAttributes;
using ETicaretApp.Application.Enums;
using ETicaretApp.Application.Features.Commands.Role.CreateRole;
using ETicaretApp.Application.Features.Commands.Role.DeleteRole;
using ETicaretApp.Application.Features.Commands.Role.UpdateRole;
using ETicaretApp.Application.Features.Queries.Role.GetRoleById;
using ETicaretApp.Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Admin")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Menu = AuthorizeDefinitionConstants.Roles, Definition = "Get Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse response = await _mediator.Send(getRolesQueryRequest);
            return Ok(response);
        }

        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Menu = AuthorizeDefinitionConstants.Roles, Definition = "Get Role By Id")]
        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse response = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Menu = AuthorizeDefinitionConstants.Roles, Definition = "Create Role")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
            CreateRoleCommandResponse response = await _mediator.Send(createRoleCommandRequest);
            return Ok(response);
        }

        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Menu = AuthorizeDefinitionConstants.Roles, Definition = "Update Role")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse response = await _mediator.Send(updateRoleCommandRequest);
            return Ok(response);
        }

        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Menu = AuthorizeDefinitionConstants.Roles, Definition = "Delete Role")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
            DeleteRoleCommandResponse response = await _mediator.Send(deleteRoleCommandRequest);
            return Ok(response);
        }
    }
}
