using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class DNA_Sequence_Converter
{
    static void Main()
    {
        Console.WriteLine("=== DNA to Binary Converter ===");

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

}