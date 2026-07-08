using SecretManager.Application.Common.Interfaces.Repositories;
using SecretManager.Domain.Entities;
using SecretManager.Infrastructure.Persistence;

namespace SecretManager.Infrastructure.Repositories.Implementation;

public class MemberRepository(AppDbContext db) : IMemberRepository
{
    public void Add(Member member) => db.Members.Add(member);

    public void Remove(Member member) => db.Members.Remove(member);
}