using System.Reflection;
using EFCoreColumnEncryption.Common.Helpers;
using EFCoreColumnEncryption.DataAccess.Attributes;
using EFCoreColumnEncryption.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreColumnEncryption.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Person> Peoples { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var encryptedProperties = modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetProperties())
            .Where(p => p.PropertyInfo.GetCustomAttribute<EncryptedAttribute>() != null);

        foreach (var property in encryptedProperties)
        {
            var attribute = property.PropertyInfo.GetCustomAttribute<EncryptedAttribute>();
            var encryptionProvider = attribute.GetEncryptionProvider<string>(); // Assuming string encryption

            property.SetMaxLength(255);
            property.SetValueConverter(
                new ValueConverter<string, string>(
                    value => encryptionProvider.Encrypt(value),
                    value => encryptionProvider.Decrypt(value)
                )
            );
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("EncryptionDB");
    }
}