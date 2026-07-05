using System;
using NUnit.Framework;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Domain;

[TestFixture]
public class AuditLogTests
{
    [Test]
    public void Record_ShouldCreateAuditLog()
    {
        var actorId = Guid.NewGuid();
        var resourceId = Guid.NewGuid();

        var log = AuditLog.Record(actorId, AuditAction.SecretCreated, "Secret", resourceId, "127.0.0.1");

        Assert.Multiple(() =>
        {
            Assert.That(log.ActorId, Is.EqualTo(actorId));
            Assert.That(log.Action, Is.EqualTo(AuditAction.SecretCreated));
            Assert.That(log.ResourceType, Is.EqualTo("Secret"));
            Assert.That(log.ResourceId, Is.EqualTo(resourceId));
            Assert.That(log.IpAddress, Is.EqualTo("127.0.0.1"));
            Assert.That(log.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(log.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }
    
    [Test]
    public void Record_ShouldAllowNullIpAddress()
    {
        var log = AuditLog.Record(Guid.NewGuid(), AuditAction.SecretRead, "Secret", Guid.NewGuid());

        Assert.That(log.IpAddress, Is.Null);
    }
}