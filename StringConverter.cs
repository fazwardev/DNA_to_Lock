using System;
using System.IO;
using System.Text;

public class StringConverter
{
    // Convert any string to binary using UTF-8 encoding
    public static string StringToBinary(string input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("Input string cannot be empty");

        string binary = "";
        byte[] bytes = Encoding.UTF8.GetBytes(input);

        foreach (byte b in bytes)
        {
            // Convert each byte to 8-bit binary
            binary += Convert.ToString(b, 2).PadLeft(8, '0');
        }

        return binary;
    }

    // Convert binary back to string using UTF-8 encoding
    public static string BinaryToString(string binary)
    {
        if (string.IsNullOrEmpty(binary))
            throw new ArgumentException("Binary string cannot be empty");

        if (binary.Length % 8 != 0)
            throw new ArgumentException("Binary string length must be a multiple of 8");

        string result = "";

        try
        {
            for (int i = 0; i < binary.Length; i += 8)
            {
                string byteBinary = binary.Substring(i, 8);
                byte b = Convert.ToByte(byteBinary, 2);
                result += (char)b;
            }
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Invalid binary string: {ex.Message}");
        }

        return result;
    }

    // Convert binary to ATGC format (00=A, 01=T, 10=G, 11=C)
    public static string BinaryToATGC(string binary)
    {
        if (string.IsNullOrEmpty(binary))
            throw new ArgumentException("Binary string cannot be empty");

        if (binary.Length % 2 != 0)
            throw new ArgumentException("Binary string length must be even for ATGC conversion");

        string result = "";

        for (int i = 0; i < binary.Length; i += 2)
        {
            string pair = binary.Substring(i, 2);
            switch (pair)
            {
                case "00":
                    result += 'A';
                    break;
                case "01":
                    result += 'T';
                    break;
                case "10":
                    result += 'G';
                    break;
                case "11":
                    result += 'C';
                    break;
                default:
                    throw new ArgumentException($"Invalid binary pair: {pair}");
            }
        }

        return result;
    }

    // Convert ATGC format back to binary
    public static string ATGCToBinary(string atgc)
    {
        if (string.IsNullOrEmpty(atgc))
            throw new ArgumentException("ATGC string cannot be empty");

        string result = "";

        foreach (char c in atgc.ToUpper())
        {
            switch (c)
            {
                case 'A':
                    result += "00";
                    break;
                case 'T':
                    result += "01";
                    break;
                case 'G':
                    result += "10";
                    break;
                case 'C':
                    result += "11";
                    break;
                default:
                    throw new ArgumentException($"Invalid ATGC character: {c}");
            }
        }

        return result;
    }

    // Complete workflow: String → Binary → Encrypt → ATGC
    public static string EncryptStringToATGC(string input, string encryptionKey)
    {
        if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(encryptionKey))
            throw new ArgumentException("Input and encryption key cannot be empty");

        // Step 1: Convert string to binary
        string binary = StringToBinary(input);
        Console.WriteLine($"Step 1 - String to Binary: {binary}");

        // Step 2: Encrypt binary
        string encrypted = EncryptionManager.Encrypt(binary, encryptionKey);
        Console.WriteLine($"Step 2 - Encrypted Binary: {encrypted}");

        // Step 3: Convert encrypted binary to ATGC
        string atgc = BinaryToATGC(encrypted);
        Console.WriteLine($"Step 3 - Encrypted as ATGC: {atgc}");

        return atgc;
    }

    // Complete workflow: ATGC → Binary → Decrypt → String
    public static string DecryptATGCToString(string atgc, string encryptionKey)
    {
        if (string.IsNullOrEmpty(atgc) || string.IsNullOrEmpty(encryptionKey))
            throw new ArgumentException("ATGC and encryption key cannot be empty");

        // Step 1: Convert ATGC to binary
        string binary = ATGCToBinary(atgc);
        Console.WriteLine($"Step 1 - ATGC to Binary: {binary}");

        // Step 2: Decrypt binary
        string decrypted = EncryptionManager.Decrypt(binary, encryptionKey);
        Console.WriteLine($"Step 2 - Decrypted Binary: {decrypted}");

        // Step 3: Convert binary to string
        string result = BinaryToString(decrypted);
        Console.WriteLine($"Step 3 - Decrypted String: {result}");

        return result;
    }

    // Encrypt string and save to file
    public static void EncryptStringToFile(string inputString, string outputFilePath, string encryptionKey)
    {
        try
        {
            Console.WriteLine("\n=== Converting String to Encrypted ATGC ===");
            string atgcResult = EncryptStringToATGC(inputString, encryptionKey);
            File.WriteAllText(outputFilePath, atgcResult);
            Console.WriteLine($"Successfully saved encrypted ATGC to: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Decrypt file and retrieve original string
    public static void DecryptFileToString(string inputFilePath, string outputFilePath, string encryptionKey)
    {
        try
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException($"File '{inputFilePath}' not found");

            string atgcData = File.ReadAllText(inputFilePath).Trim();
            Console.WriteLine("\n=== Converting Encrypted ATGC to String ===");
            string decryptedString = DecryptATGCToString(atgcData, encryptionKey);
            File.WriteAllText(outputFilePath, decryptedString);
            Console.WriteLine($"Successfully saved decrypted string to: {outputFilePath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
