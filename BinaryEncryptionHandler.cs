using System;
using System.IO;

public class BinaryEncryptionHandler
{
    // Handle binary data encryption
    public static void EncryptBinaryDataMenu()
    {
        Console.WriteLine("Enter input file path (binary data): ");
        string inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile))
            inputFile = "output.txt";

        Console.WriteLine("Enter encryption key (any string): ");
        string key = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(key))
        {
            Console.WriteLine("Error: Encryption key cannot be empty!");
            return;
        }

        Console.WriteLine("Enter output file path (or leave empty for 'encrypted.txt'): ");
        string outputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputFile))
            outputFile = "encrypted.txt";

        EncryptionManager.EncryptFile(inputFile, outputFile, key);
    }

    // Handle binary data decryption
    public static void DecryptBinaryDataMenu()
    {
        Console.WriteLine("Enter input file path (encrypted data): ");
        string inputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputFile))
            inputFile = "encrypted.txt";

        Console.WriteLine("Enter decryption key (must match encryption key): ");
        string key = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(key))
        {
            Console.WriteLine("Error: Decryption key cannot be empty!");
            return;
        }

        Console.WriteLine("Enter output file path (or leave empty for 'decrypted.txt'): ");
        string outputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputFile))
            outputFile = "decrypted.txt";

        EncryptionManager.DecryptFile(inputFile, outputFile, key);
    }
}
