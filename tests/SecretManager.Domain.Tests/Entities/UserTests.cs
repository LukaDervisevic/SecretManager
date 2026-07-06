using System;
using NUnit.Framework;
using SecretManager.Domain.Entities;

namespace SecretManager.Domain;

[TestFixture]
public class UserTests
{
    [Test]
    public void Create_ShouldCreateUser_WithCorrectProperties()
    {
        var user = User.Create("Test@Example.COM", "hashedpassword", "publickey", "encryptedprivatekey","mastersalt");
        
        Assert.Multiple(() =>
        {
            Assert.That(user.Email, Is.EqualTo("test@example.com"));
            Assert.That(user.PasswordHash, Is.EqualTo("hashedpassword"));
            Assert.That(user.PublicKey, Is.EqualTo("publickey"));
            Assert.That(user.EncryptedPrivateKey, Is.EqualTo("encryptedprivatekey"));
            Assert.That(user.Id, Is.Not.EqualTo(Guid.Empty));
            Assert.That(user.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Create_ShouldFormatEmail_ToLowercase()
    {
        var user = User.Create("  TEST@EXAMPLE.COM  ", "hash", "pub", "priv","mastersalt");
        Assert.That(user.Email, Is.EqualTo("test@example.com"));
    }

    [Test]
    public void UpdatePasswordHash_ShouldUpdateHash()
    {
        var user = User.Create("test@example.com", "oldhash", "pub", "priv","mastersalt");
        user.UpdatePasswordHash("newhash");
        Assert.That(user.PasswordHash, Is.EqualTo("newhash"));
    }
}