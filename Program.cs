using System;

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
            Console.WriteLine("5. Encrypt String to ATGC");
            Console.WriteLine("6. Decrypt ATGC to String");
            Console.WriteLine("7. Exit");
            Console.Write("Choose option (1-7): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DNABinaryConverter.DNAToBinaryMenu();
                    break;
                case "2":
                    DNABinaryConverter.BinaryToDNAMenu();
                    break;
                case "3":
                    BinaryEncryptionHandler.EncryptBinaryDataMenu();
                    break;
                case "4":
                    BinaryEncryptionHandler.DecryptBinaryDataMenu();
                    break;
                case "5":
                    StringEncryptionHandler.EncryptStringToATGCMenu();
                    break;
                case "6":
                    StringEncryptionHandler.DecryptATGCToStringMenu();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Exiting... Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please try again.");
                    break;
            }
        }
    }
}