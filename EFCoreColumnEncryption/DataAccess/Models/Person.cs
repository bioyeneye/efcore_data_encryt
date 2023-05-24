using EFCoreColumnEncryption.Common.Helpers;
using EFCoreColumnEncryption.DataAccess.Attributes;

namespace EFCoreColumnEncryption.DataAccess.Models;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    [Encrypted(typeof(StringEncryptionProvider))]
    public string SSN { get; set; }
}