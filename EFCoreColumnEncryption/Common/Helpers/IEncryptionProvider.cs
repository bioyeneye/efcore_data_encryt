namespace EFCoreColumnEncryption.Common.Helpers;

public interface IEncryptionProvider<T>
{
    string Encrypt(T input);
    T Decrypt(string encryptedInput);
}