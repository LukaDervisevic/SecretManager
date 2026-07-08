using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Features.Secrets.Commands.CreateSecret;
using SecretManager.Application.Features.Secrets.Commands.DeleteSecret;
using SecretManager.Application.Features.Secrets.Commands.UpdateSecret;
using SecretManager.Application.Features.Secrets.Queries.ListVaultSecrets;

namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SecretsController(ISender sender) : ControllerBase
{
    
    [HttpGet("vault/{vaultId:guid}")]
    public async Task<IActionResult> ListByVault(Guid vaultId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 50, CancellationToken ct = default)
    {
        var result = await sender.Send(new ListSecretsByVaultQuery(vaultId, pageNumber, pageSize), ct);
        return result.IsSuccess
            ? Ok(Response<List<SecretDto>>.Ok(result.Value!))
            : BadRequest(Response<List<SecretDto>>.Fail(result.Error!));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSecretCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Create), new { id = result.Value }, Response<Guid>.Ok(result.Value))
            : BadRequest(Response<Guid>.Fail(result.Error!));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateSecretCommand command, CancellationToken ct)
    {
        if (id != command.SecretId)
            return BadRequest(Response<object>.Fail("Id in url does not match Id in body."));

        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteSecretCommand(id), ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!));
    }
    
}