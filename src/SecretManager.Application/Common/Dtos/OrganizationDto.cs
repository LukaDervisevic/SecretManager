namespace SecretManager.Application.Common.Dtos;

public record OrganizationDto(
    Guid Id,
    string Name,
    Guid OwnerId,
    DateTime CreatedAt,
    List<MemberDto> Members,
    List<VaultDto> Vaults
    );