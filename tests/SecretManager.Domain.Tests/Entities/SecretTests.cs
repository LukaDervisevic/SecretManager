using System;
using NUnit.Framework;
using SecretManager.Domain.Entities;
using SecretManager.Domain.Enums;

namespace SecretManager.Domain;

public class SecretTests
{
    [Test]
    public void Create_ShouldCreateSecret()
    {
        var vaultId = Guid.NewGuid();
        var ownerId = Guid.NewGuid();

        var secret = Secret.Create("My Password", SecretType.Login, "ciphertext", ownerId, vaultId);

        Assert.Multiple(() =>
        {
            Assert.That(secret.Name, Is.EqualTo("My Password"));
            Assert.That(secret.Type, Is.EqualTo(SecretType.Login));
            Assert.That(secret.CiphertextBlob, Is.EqualTo("ciphertext"));
            Assert.That(secret.VaultId, Is.EqualTo(vaultId));
            Assert.That(secret.CollectionId, Is.Null);
            Assert.That(secret.Id, Is.Not.EqualTo(Guid.Empty));
        });
    }
    
    [Test]
    public void Create_ShouldSetCollectionId()
    {
        var collectionId = Guid.NewGuid();

        var secret = Secret.Create("My Password", SecretType.Login, "ciphertext", Guid.NewGuid(), collectionId);

        Assert.That(secret.CollectionId, Is.EqualTo(collectionId));
    }
    
    [Test]
    public void Rename_ShouldUpdateName_AndUpdatedAt()
    {
        var secret = Secret.Create("Old Name", SecretType.Login, "ciphertext", Guid.NewGuid(), Guid.NewGuid());
        var beforeUpdate = DateTime.UtcNow;

        secret.Rename("New Name");

        Assert.Multiple(() =>
        {
            Assert.That(secret.Name, Is.EqualTo("New Name"));
            Assert.That(secret.UpdatedAt, Is.EqualTo(beforeUpdate).Within(TimeSpan.FromSeconds(1)));
        });
    }
    
    [Test]
    public void UpdateCiphertext_AndUpdatedAt()
    {
        var secret = Secret.Create("Name", SecretType.Login, "old-ciphertext", Guid.NewGuid(), Guid.NewGuid());
        var beforeUpdate = DateTime.UtcNow;

        secret.UpdateCiphertext("new-ciphertext");

        Assert.Multiple(() =>
        {
            Assert.That(secret.CiphertextBlob, Is.EqualTo("new-ciphertext"));
            Assert.That(secret.UpdatedAt, Is.EqualTo(beforeUpdate).Within(TimeSpan.FromSeconds(1)));
        });
    }
    
    [Test]
    public void MoveToCollection_ShouldAllowNull_ToRemoveFromCollection()
    {
        var secret = Secret.Create("Name", SecretType.Login, "ciphertext", Guid.NewGuid(), Guid.NewGuid());

        secret.MoveToCollection(null);

        Assert.That(secret.CollectionId, Is.Null);
    }
}