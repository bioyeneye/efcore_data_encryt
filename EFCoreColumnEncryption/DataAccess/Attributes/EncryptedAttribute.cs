using EFCoreColumnEncryption.Common.Helpers;

namespace EFCoreColumnEncryption.DataAccess.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class EncryptedAttribute : Attribute
{
    private readonly Type _encryptionProviderType;

    public EncryptedAttribute(Type encryptionProviderType)
    {
        _encryptionProviderType = encryptionProviderType;
    }

    public IEncryptionProvider<T> GetEncryptionProvider<T>()
    {
        if (!typeof(IEncryptionProvider<T>).IsAssignableFrom(_encryptionProviderType))
        {
            throw new InvalidOperationException($"The encryption provider {_encryptionProviderType.Name} does not implement IEncryptionProvider<{typeof(T).Name}>.");
        }

        return (IEncryptionProvider<T>)Activator.CreateInstance(_encryptionProviderType);
    }
}