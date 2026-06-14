using System;
using System.IO;

public class EncryptionManager
{
    // Encrypt binary string using XOR cipher with a key
    public static string Encrypt(string binaryData, string encryptionKey)
    {
        if (string.IsNullOrEmpty(binaryData) || string.IsNullOrEmpty(encryptionKey))
            throw new ArgumentException("Binary data and encryption key cannot be empty");

        string encrypted = "";
        int keyIndex = 0;

        for (int i = 0; i < binaryData.Length; i++)
        {
            // Get binary digit (0 or 1)
            int bit = int.Parse(binaryData[i].ToString());

            // Get key character and convert to binary (use last bit)
            char keyChar = encryptionKey[keyIndex % encryptionKey.Length];
            int keyBit = (int)keyChar % 2;

            // XOR operation: 0 XOR 0 = 0, 0 XOR 1 = 1, 1 XOR 0 = 1, 1 XOR 1 = 0
            int encryptedBit = bit ^ keyBit;
            encrypted += encryptedBit;

            keyIndex++;
        }

        return encrypted;
    }

    // Decrypt binary string using XOR cipher with a key (XOR is symmetric)
    public static string Decrypt(string encryptedData, string encryptionKey)
    {
        if (string.IsNullOrEmpty(encryptedData) || string.IsNullOrEmpty(encryptionKey))
            throw new ArgumentException("Encrypted data and encryption key cannot be empty");

        // XOR decryption is the same as encryption
        return Encrypt(encryptedData, encryptionKey);
    }

    // Encrypt file and save to new file
    public static void EncryptFile(string inputFilePath, string outputFilePath, string encryptionKey)
    {
        try
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException($"Input file '{inputFilePath}' not found!");

            string binaryData = File.ReadAllText(inputFilePath).Trim();
            Console.WriteLine($"Encrypting data from: {inputFilePath}");

            string encryptedData = Encrypt(binaryData, encryptionKey);

            File.WriteAllText(outputFilePath, encryptedData);
            Console.WriteLine($"Encryption successful! Output saved to: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Encryption error: {ex.Message}");
        }
    }

    // Decrypt file and save to new file
    public static void DecryptFile(string inputFilePath, string outputFilePath, string encryptionKey)
    {
        try
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException($"Input file '{inputFilePath}' not found!");

            string encryptedData = File.ReadAllText(inputFilePath).Trim();
            Console.WriteLine($"Decrypting data from: {inputFilePath}");

            string decryptedData = Decrypt(encryptedData, encryptionKey);

            File.WriteAllText(outputFilePath, decryptedData);
            Console.WriteLine($"Decryption successful! Output saved to: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Decryption error: {ex.Message}");
        }
    }

    // Display encryption/decryption menu
    public static void ShowMenu()
    {
        Console.WriteLine("\n=== Encryption/Decryption Menu ===");
        Console.WriteLine("1. Encrypt file");
        Console.WriteLine("2. Decrypt file");
        Console.WriteLine("3. Exit");
        Console.Write("Choose option (1-3): ");
    }
}
