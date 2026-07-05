using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecretManager.API.Models;
using SecretManager.Application.Auth.Commands.Login;
using SecretManager.Application.Auth.Commands.Register;

namespace SecretManager.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ISender sender) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    {
        var result =  await sender.Send(command, ct);
        return result.IsSuccess
            ? Ok(Response<AuthResponse>.Ok(result.Value!))
            : BadRequest(Response<AuthResponse>.Fail(result.Error!));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return result.IsSuccess
            ? Ok(Response<AuthResponse>.Ok(result.Value!))
            : BadRequest(Response<AuthResponse>.Fail(result.Error!));
    }
    
}