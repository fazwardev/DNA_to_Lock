using System;
using System.IO;

public class StringEncryptionHandler
{
    // Handle string encryption workflow - Direct text input
    public static void EncryptStringToATGCMenu()
    {
        Console.WriteLine("\n=== Encrypt String to ATGC ===");
        Console.WriteLine("1. Encrypt from text input");
        Console.WriteLine("2. Encrypt from .txt file");
        Console.Write("Choose option (1 or 2): ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                EncryptStringDirectInput();
                break;
            case "2":
                EncryptStringFromFile();
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }

    // Encrypt string from direct text input
    private static void EncryptStringDirectInput()
    {
        Console.WriteLine("\n=== Encrypt String (Direct Input) ===");
        Console.WriteLine("Enter the string to encrypt: ");
        string inputString = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputString))
        {
            Console.WriteLine("Error: Input string cannot be empty!");
            return;
        }

        Console.WriteLine("Enter encryption key: ");
        string key = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(key))
        {
            Console.WriteLine("Error: Encryption key cannot be empty!");
            return;
        }

        Console.WriteLine("Enter output file path (or leave empty for 'encrypted_string.txt'): ");
        string outputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputFile))
            outputFile = "encrypted_string.txt";

        try
        {
            StringConverter.EncryptStringToFile(inputString, outputFile, key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Encrypt string from .txt file
    private static void EncryptStringFromFile()
    {
        Console.WriteLine("\n=== Encrypt String (From File) ===");
        Console.WriteLine("Enter input file path (or leave empty for 'input_string.txt'): ");
        string inputFile = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputFile))
            inputFile = "input_string.txt";

        if (!File.Exists(inputFile))
        {
            Console.WriteLine($"Error: File '{inputFile}' not found!");
            return;
        }

        Console.WriteLine("Enter encryption key: ");
        string key = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(key))
        {
            Console.WriteLine("Error: Encryption key cannot be empty!");
            return;
        }

        Console.WriteLine("Enter output file path (or leave empty for 'encrypted_string.txt'): ");
        string outputFile = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputFile))
            outputFile = "encrypted_string.txt";

        try
        {
            // Read string from input file
            string inputString = File.ReadAllText(inputFile).Trim();
            Console.WriteLine($"\nRead from '{inputFile}': {inputString}");

            // Encrypt and save to output file
            StringConverter.EncryptStringToFile(inputString, outputFile, key);
            Console.WriteLine($"Successfully encrypted and saved to: {outputFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Handle string decryption workflow
    public static void DecryptATGCToStringMenu()
    {
        Console.WriteLine("\n=== Decrypt ATGC to String ===");
        Console.WriteLine("Enter input file path (or leave empty for 'encrypted_string.txt'): ");
        string inputFile = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(inputFile))
            inputFile = "encrypted_string.txt";

        Console.WriteLine("Enter decryption key (must match encryption key): ");
        string key = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(key))
        {
            Console.WriteLine("Error: Decryption key cannot be empty!");
            return;
        }

        Console.WriteLine("Enter output file path (or leave empty for 'decrypted_string.txt'): ");
        string outputFile = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(outputFile))
            outputFile = "decrypted_string.txt";

        try
        {
            StringConverter.DecryptFileToString(inputFile, outputFile, key);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
