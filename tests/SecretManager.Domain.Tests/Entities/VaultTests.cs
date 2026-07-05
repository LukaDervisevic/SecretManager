using System;
using NUnit.Framework;
using SecretManager.Domain.Entities;

namespace SecretManager.Domain;

[TestFixture]
public class VaultTests
{
    [Test]
    public void Create_ShouldCreateVault()
    {
        var ownerId = Guid.NewGuid();
        var vault = Vault.Create("My Vault", ownerId, "encryptedkey");
        
        Assert.Multiple((() =>
        {
            Assert.That(vault.Name, Is.EqualTo("My Vault"));
            Assert.That(vault.OwnerId, Is.EqualTo(ownerId));
            Assert.That(vault.EncryptedKey,Is.EqualTo("encryptedkey"));
            Assert.That(vault.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(vault.CreatedAt,Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(vault.Secrets,Is.Empty);
        }));
    }

    [Test]
    public void Rename_ShouldUpdateName()
    {
        var vault = Vault.Create("oldname", Guid.NewGuid(), "encryptedkey");
        vault.Rename("newname");
        Assert.That(vault.Name,Is.EqualTo("newname"));
    }
}