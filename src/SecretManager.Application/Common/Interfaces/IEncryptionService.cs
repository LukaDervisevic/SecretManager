namespace SecretManager.Application.Common.Interfaces;

public interface IEncryptionService
{
    string Encrypt(string plaintext, string key);
    string Decrypt(string ciphertext, string key);
    (string PublicKey, string PrivateKey) GenerateKeyPair(string password);
}