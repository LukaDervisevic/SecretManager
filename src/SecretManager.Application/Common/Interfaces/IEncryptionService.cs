namespace SecretManager.Application.Common.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string plaintext, string key);
    string Decrypt(string ciphertext, string key);
    KeyPairResult GenerateKeyPair(string password);
    string GenerateVaultKey(string password, string salt);
    string EncryptVaultKey(string vaultKey, string passwordDerivedKey);
}

public record KeyPairResult(
    string PublicKey,
    string EncryptedPrivateKey,
    string Salt,              
    string PasswordDerivedKey
    );