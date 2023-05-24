// See https://aka.ms/new-console-template for more information

using EFCoreColumnEncryption.Common.Helpers;
using EFCoreColumnEncryption.DataAccess;
using EFCoreColumnEncryption.DataAccess.Models;
using Microsoft.Extensions.DependencyInjection;


// Create a new DI container
var serviceProvider = new ServiceCollection()
    .AddTransient<IEncryptionProvider<string>, StringEncryptionProvider>()
    .AddDbContext<AppDbContext>()
    .BuildServiceProvider();

// Resolve the AppDbContext from the DI container
var dbContext = serviceProvider.GetService<AppDbContext>();

// Use the dbContext to perform operations
var person = new Person { Name = "John Doe", SSN = "12345678"};
dbContext.Peoples.Add(person);
dbContext.SaveChanges();

var retrievedPerson = dbContext.Peoples.FirstOrDefault();
Console.WriteLine($"Retrieved Name: {retrievedPerson?.Name}");

Console.ReadLine();