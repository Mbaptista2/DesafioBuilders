using System;

namespace Palindrome
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.Write($"Recursão para checar palindromo : {Environment.NewLine}");

            Console.Write($"Insira a palavra : {Environment.NewLine}");
            var word = Console.ReadLine();
            var result = Verify(word.ToLower());
            if (result)
                Console.WriteLine($"A palavra {word} é um palindromo");
            else
                Console.WriteLine($"A palavra {word} não é um palindromo");
        }

        public static bool Verify(string word)
        {
            if (word.Length <= 1)
                return true;
            else
            {
                if (word[0] != word[word.Length - 1])
                    return false;
                else
                    return Verify(word.Substring(1, word.Length - 2));
            }
        }
    }
}