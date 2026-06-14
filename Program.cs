using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class DNA_Sequence_Converter
{
    static void Main()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== DNA to Lock System ===");
            Console.WriteLine("1. Convert DNA to Binary");
            Console.WriteLine("2. Convert Binary to DNA");
            Console.WriteLine("3. Encrypt Binary Data");
            Console.WriteLine("4. Decrypt Binary Data");
            Console.WriteLine("5. Exit");
            Console.Write("Choose option (1-5): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ConvertDNAToBinaryMenu();
                    break;
                case "2":
                    ConvertBinaryToDNAMenu();
                    break;
                case "3":
                    EncryptionManager.ShowMenu();
                    string encryptChoice = Console.ReadLine();
                    if (encryptChoice == "1")
                        HandleEncryption();
                    break;
                case "4":
                    HandleDecryption();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Exiting... Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        }
    }

    static void ConvertDNAToBinaryMenu()
    {
        Console.WriteLine("\n=== DNA to Binary Converter ===");

        // Input file path
        Console.WriteLine("Enter input .txt file path (or leave empty for 'input.txt'): ");
        string inputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPath))
            inputPath = "input.txt";

        // Output file path
        Console.WriteLine("Enter output .txt file path (or leave empty for 'output.txt'): ");
        string outputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputPath))
            outputPath = "output.txt";

        try
        {
            // Read DNA sequence from file
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: File '{inputPath}' not found!");
                return;
            }

            string dnaSequence = File.ReadAllText(inputPath).Trim();
            Console.WriteLine($"\nRead DNA sequence: {dnaSequence}");

            // Convert DNA to binary
            string binaryResult = ConvertDNAToBinary(dnaSequence);
            Console.WriteLine($"Converted to binary: {binaryResult}");

            // Write result to output file
            File.WriteAllText(outputPath, binaryResult);
            Console.WriteLine($"\nSuccess! Output saved to '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ConvertBinaryToDNAMenu()
    {
        Console.WriteLine("\n=== Binary to DNA Converter ===");

        // Input file path
        Console.WriteLine("Enter input .txt file path (or leave empty for 'output.txt'): ");
        string inputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(inputPath))
            inputPath = "output.txt";

        // Output file path
        Console.WriteLine("Enter output .txt file path (or leave empty for 'dna_output.txt'): ");
        string outputPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(outputPath))
            outputPath = "dna_output.txt";

        try
        {
            // Read binary sequence from file
            if (!File.Exists(inputPath))
            {
                Console.WriteLine($"Error: File '{inputPath}' not found!");
                return;
            }

            string binarySequence = File.ReadAllText(inputPath).Trim();
            Console.WriteLine($"\nRead binary sequence: {binarySequence}");

            // Convert binary to DNA
            string dnaResult = ConvertBinaryToDNA(binarySequence);
            Console.WriteLine($"Converted to DNA: {dnaResult}");

            // Write result to output file
            File.WriteAllText(outputPath, dnaResult);
            Console.WriteLine($"\nSuccess! Output saved to '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void HandleEncryption()
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

    static void HandleDecryption()
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

    // Convert DNA sequence to binary
    static string ConvertDNAToBinary(string dnaSequence)
    {
        // Create mapping: A=00, T=01, G=10, C=11
        Dictionary<char, string> dnaMapping = new Dictionary<char, string>
        {
            { 'A', "00" },
            { 'T', "01" },
            { 'G', "10" },
            { 'C', "11" }
        };

        string result = "";
        foreach (char c in dnaSequence.ToUpper())
        {
            if (dnaMapping.ContainsKey(c))
                result += dnaMapping[c];
            else if (!char.IsWhiteSpace(c))
                Console.WriteLine($"Warning: Unknown character '{c}' - skipping");
        }

        return result;
    }

    // Convert binary sequence to DNA
    static string ConvertBinaryToDNA(string binarySequence)
    {
        // Create reverse mapping: 00=A, 01=T, 10=G, 11=C
        Dictionary<string, char> reverseMapping = new Dictionary<string, char>
        {
            { "00", 'A' },
            { "01", 'T' },
            { "10", 'G' },
            { "11", 'C' }
        };

        string result = "";

        // Process binary string in pairs (2 bits at a time)
        for (int i = 0; i < binarySequence.Length; i += 2)
        {
            if (i + 1 < binarySequence.Length)
            {
                string pair = binarySequence.Substring(i, 2);
                if (reverseMapping.ContainsKey(pair))
                    result += reverseMapping[pair];
                else
                    Console.WriteLine($"Warning: Unknown binary pair '{pair}' - skipping");
            }
            else if (i < binarySequence.Length)
            {
                Console.WriteLine($"Warning: Incomplete binary pair at position {i} - skipping");
            }
        }

        return result;
    }

}