using SecretManager.Domain.Enums;

namespace SecretManager.Application.Common.Dtos;

public record MemberDto(
    Guid Id,
    Guid OrganizationId,
    Guid UserId,
    AccessPolicy Role,
    DateTime JoinedAt
    );