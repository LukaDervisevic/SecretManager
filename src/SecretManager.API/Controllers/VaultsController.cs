using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Features.Vaults.Commands.CreateVault;
using SecretManager.Application.Features.Vaults.Commands.DeleteVault;
using SecretManager.Application.Features.Vaults.Queries.ListOrganizationsVaults;
using SecretManager.Application.Features.Vaults.Queries.ListUsersVaults;

namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class VaultsController(ISender sender) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(CreateVaultCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Create), new { id = result.Value }, Response<Guid>.Ok(result.Value))
            : BadRequest(Response<Guid>.Fail(result.Error!));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteVaultCommand(id), ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<Guid>.Fail(result.Error!));
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUsersVaults(CancellationToken ct)
    {
        var result = await sender.Send(new ListUsersVaultsQuery(), ct);
        return result.IsSuccess
            ? Ok(Response<List<VaultDto>>.Ok(result.Value!))
            : BadRequest(Response<List<VaultDto>>.Fail(result.Error!));
    }

    [HttpGet("organization/{id:guid}")]
    public async Task<IActionResult> GetOrganizationsVaults(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new ListOrganizationsVaultsQuery(id), ct);
        return result.IsSuccess
            ? Ok(Response<List<VaultDto>>.Ok(result.Value!))
            : BadRequest(Response<List<VaultDto>>.Fail(result.Error!));
    }
}