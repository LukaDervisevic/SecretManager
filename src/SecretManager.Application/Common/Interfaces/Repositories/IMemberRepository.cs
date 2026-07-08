using SecretManager.Domain.Entities;

namespace SecretManager.Application.Common.Interfaces.Repositories;

public interface IMemberRepository
{
    void Add(Member member);
    void Remove(Member member);
}