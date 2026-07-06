using System.Security.Cryptography;
using System.Text;
using SecretManager.Application.Common.Interfaces;

namespace SecretManager.Infrastructure.Security;

public class EncryptionService : IEncryptionService
{
    public string Encrypt(string plaintext, string key)
    {
        var keyBytes = Convert.FromBase64String(key);
        var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
        RandomNumberGenerator.Fill(nonce);

        var plainTextBytes = Encoding.UTF8.GetBytes(plaintext);
        var cipherText = new byte[plainTextBytes.Length];
        var tag = new byte[AesGcm.TagByteSizes.MaxSize];

        using var aes = new AesGcm(keyBytes, AesGcm.TagByteSizes.MaxSize);
        aes.Encrypt(nonce, plainTextBytes, cipherText, tag);

        var result = new byte[nonce.Length + tag.Length + cipherText.Length];
        nonce.CopyTo(result, 0);
        tag.CopyTo(result, nonce.Length);
        cipherText.CopyTo(result, nonce.Length + tag.Length);

        return Convert.ToBase64String(result);
    }

    public string Decrypt(string ciphertext, string key)
    {
        var keyBytes = Convert.FromBase64String(key);
        var data = Convert.FromBase64String(ciphertext);

        var nonceSize = AesGcm.NonceByteSizes.MaxSize;
        var tagSize = AesGcm.TagByteSizes.MaxSize;

        var nonce = new byte[nonceSize];
        var tag = new byte[tagSize];
        var cipherTxt = new byte[data.Length - nonceSize - tagSize];

        Buffer.BlockCopy(data, 0, nonce, 0, nonceSize);
        Buffer.BlockCopy(data, nonceSize, tag, 0, tagSize);
        Buffer.BlockCopy(data, nonceSize + tagSize, cipherTxt, 0, cipherTxt.Length);

        var plaintext = new byte[cipherTxt.Length];

        using var aes = new AesGcm(keyBytes, tagSize);
        aes.Decrypt(nonce, cipherTxt, tag, plaintext);

        return Encoding.UTF8.GetString(plaintext);
    }

    public KeyPairResult GenerateKeyPair(string password)
    {
        var privateKeyBytes = new byte[32];
        RandomNumberGenerator.Fill(privateKeyBytes);
        var privateKey = Convert.ToBase64String(privateKeyBytes);

        var saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);
        var salt = Convert.ToBase64String(saltBytes);

        var passwordDerivedKey = DeriveKeyFromPassword(password, saltBytes);

        var encryptedPrivateKey = Encrypt(privateKey, passwordDerivedKey);

        var publicKeyBytes = new byte[32];
        RandomNumberGenerator.Fill(publicKeyBytes);
        var publicKey = Convert.ToBase64String(publicKeyBytes);

        return new KeyPairResult(publicKey, encryptedPrivateKey, salt, passwordDerivedKey);
    }
    
    public string GenerateVaultKey(string password, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        return DeriveKeyFromPassword(password, saltBytes);
    }
    
    public string EncryptVaultKey(string vaultKey, string passwordDerivedKey) =>
        Encrypt(vaultKey, passwordDerivedKey);
    
    private static string DeriveKeyFromPassword(string password, byte[] salt) =>
        Convert.ToBase64String(
            Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations: 100_000,
                HashAlgorithmName.SHA256,
                outputLength: 32));
}