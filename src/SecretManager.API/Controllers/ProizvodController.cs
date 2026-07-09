using MediatR;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Features.Proizvod.Commands;

namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProizvodController(ISender sender): ControllerBase
{
    [HttpPut("azuriraj-proizvod")]
    public async Task<IActionResult> UpdateProizvod(UpdateProizvodCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? NoContent()
            : BadRequest(Response<object>.Fail(result.Error!)); 
    }
}