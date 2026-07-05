using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Features.Collections.Commands.CreateCollection;
using SecretManager.Application.Features.Collections.Commands.DeleteCollection;
using SecretManager.Application.Features.Collections.Queries.ListCollectionsByVault;

namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CollectionsController(ISender sender) : ControllerBase
{
    [HttpGet("vault/{vaultId:guid}")]
    public async Task<IActionResult> ListByVault(Guid vaultId, CancellationToken ct)
    {
        var result = await sender.Send(new ListCollectionsByVaultQuery(vaultId), ct);
        return result.IsSuccess
            ? Ok(Response<List<CollectionDto>>.Ok(result.Value!))
            : BadRequest(Response<List<CollectionDto>>.Fail(result.Error!));
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateCollectionCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(ListByVault), new { vaultId = command.VaultId }, Response<Guid>.Ok(result.Value))
            : BadRequest(Response<Guid>.Fail(result.Error!));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await sender.Send(new DeleteCollectionCommand(id), ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!));
    }
    
}