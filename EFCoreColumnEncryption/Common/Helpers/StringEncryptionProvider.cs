namespace EFCoreColumnEncryption.Common.Helpers;

public class StringEncryptionProvider : IEncryptionProvider<string>
{
    public string Encrypt(string input)
    {
        // Implement string encryption logic here
        // Example: Using a simple XOR encryption
        var encryptedChars = input.ToCharArray();
        for (int i = 0; i < encryptedChars.Length; i++)
        {
            encryptedChars[i] = (char)(encryptedChars[i] ^ 0xFF);
        }
        var result = new string(encryptedChars);
        return result;
    }

    public string Decrypt(string encryptedInput)
    {
        // Implement string decryption logic here
        // Example: Using the reverse operation of XOR encryption
        return Encrypt(encryptedInput); // XOR encryption is symmetric
    }
}