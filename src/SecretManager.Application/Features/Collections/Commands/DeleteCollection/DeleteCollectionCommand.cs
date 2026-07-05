using MediatR;
using SecretManager.Application.Common.Models;

namespace SecretManager.Application.Features.Collections.Commands.DeleteCollection;

public record DeleteCollectionCommand(Guid CollectionId): IRequest<Result>;