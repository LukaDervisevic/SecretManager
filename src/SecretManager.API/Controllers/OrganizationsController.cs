using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Features.Organizations.Commands.AddOrganizationMember;
using SecretManager.Application.Features.Organizations.Commands.CreateOrganization;
using SecretManager.Application.Features.Organizations.Commands.RemoveOrganizationMember;
using SecretManager.Application.Features.Organizations.Queries.ListUsersOrganizations;


namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrganizationsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateOrganizationCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? Ok(Response<object>.Ok(result.Value))
            : BadRequest(Response<object>.Fail(result.Error!));

    }

    [HttpPost("{id:guid}/members")]
    public async Task<IActionResult> AddMember(Guid id, AddOrganizationMemberCommand command, CancellationToken ct)
    {
        if (id != command.OrganizationId)
            return BadRequest(Response<object>.Fail("Id in URL does not match Id in body."));

        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!));
    }
    
    [HttpDelete("{id:guid}/members/{userId:guid}")]
    public async Task<IActionResult> RemoveMember(Guid id, Guid userId, CancellationToken ct)
    {
        var result = await sender.Send(new RemoveOrganizationMemberCommand(id,userId), ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!));
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUsersOrganizations(CancellationToken ct)
    {
        var result = await sender.Send(new ListUsersOrganizationsQuery(), ct);
        return result.IsSuccess
            ? Ok(Response<List<OrganizationDto>>.Ok(result.Value!))
            : BadRequest(Response<List<OrganizationDto>>.Fail(result.Error!));
    }
    
}