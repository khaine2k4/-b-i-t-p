// A utility to analyze text files and provide statistics
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace FileAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("File Analyzer - .NET Core");
            Console.WriteLine("This tool analyzes text files and provides statistics.");
            
            if (args.Length == 0)
            {
                Console.WriteLine("Please provide a file path as a command-line argument.");
                Console.WriteLine("Example: dotnet run myfile.txt");
                return;
            }
            
            string filePath = args[0];
            
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File '{filePath}' does not exist.");
                return;
            }
            
            try
            {
                Console.WriteLine($"Analyzing file: {filePath}");
                
                // Read the file content
                string content = File.ReadAllText(filePath);

                // TODO: Implement analysis functionality

                // 1. Count words
                
                string[] words = content.Split(new[] { ' ', '\r', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                int wordCount = words.Length;
                Console.WriteLine($"Number of words: {wordCount}");

                // 2. Count characters (with and without whitespace)
                int charWithSpace = content.Length;
                int charWithoutSpace = content.Count(c => !char.IsWhiteSpace(c));
                Console.WriteLine($"Characters (with spaces): {charWithSpace}");
                Console.WriteLine($"Characters (without spaces): {charWithoutSpace}");

                // 3. Count sentences
                int sentenceCount = content.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
                Console.WriteLine($"Number of sentences: {sentenceCount}");

                // 4. Identify most common words
                var wordFrequency = words
                    .Select(w => w.Trim().ToLower())
                    .Where(w => w.All(char.IsLetterOrDigit))
                    .GroupBy(w => w)
                    .ToDictionary(g => g.Key, g => g.Count());

                var topWords = wordFrequency
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(5);

                Console.WriteLine("Top 5 most common words:");
                foreach (var pair in topWords)
                {
                    Console.WriteLine($"{pair.Key}: {pair.Value} times");
                }

                // 5. Average word length
                double averageWordLength = words.Any() ? words.Average(w => w.Length) : 0;
                Console.WriteLine($"Average word length: {averageWordLength:F2}");


                // Example implementation for counting lines:
                int lineCount = File.ReadAllLines(filePath).Length;
                Console.WriteLine($"Number of lines: {lineCount}");
                
                // TODO: Additional analysis to be implemented
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file analysis: {ex.Message}");
            }
        }
    }
}