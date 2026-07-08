using SecretManager.Application.Common.Dtos;
using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Mappings;

public static class MemberMappings
{
    public static MemberDto ToDto(this Member member) => new(
        member.Id,
        member.OrganizationId,
        member.UserId,
        member.Role,
        member.JoinedAt
    );
}
