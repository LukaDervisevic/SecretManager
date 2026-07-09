using MediatR;
using SecretManager.Application.Common.Dtos;
using SecretManager.Application.Common.Interfaces;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Proizvod.Commands;

public class UpdateProizvodHandler(IUnitOfWork uow): IRequestHandler<UpdateProizvodCommand,Result<Domain.Entities.Proizvod>>
{
    public async Task<Result<Domain.Entities.Proizvod>> Handle(UpdateProizvodCommand request, CancellationToken cancellationToken)
    {
        var proizvodToUpdate = Domain.Entities.Proizvod.Create(request.Id, request.Name, request.Cena);
        var updatedProizvod = uow.ProizvodRepository.Update(proizvodToUpdate);
        await uow.SaveChangesAsync(cancellationToken);
        return Result<Domain.Entities.Proizvod>.Success(updatedProizvod);
    }
}