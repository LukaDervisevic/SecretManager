using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Proizvod.Commands;

public record UpdateProizvodCommand(Guid Id, string Name, double Cena): IRequest<Result<Domain.Entities.Proizvod>>;
