using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WordleSolver.Models;

namespace WordleSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var logFile = File.ReadAllLines(@"C:\Users\joe\OneDrive\Documents\Development\WordleSolver\Words.txt");
            var wordList = new List<string>(logFile);
            var guesses = new List<Letter>();

            Console.WriteLine($"Total words possible: {wordList.Count()}");

            for (int i = 0; i <= 5 - 1; i++)
            {
                var guess = ReadGuesses(i);
                if (guess != null)
                {
                    guesses.Add(guess);
                }
            }

            foreach (var guess in guesses)
            {
                wordList = wordList.Where(s => s.Contains(guess.Character)).ToList();

                if (guess.Position != null)
                {
                    wordList = wordList.Where(s => s[(int)guess.Position] == guess.Character).ToList();
                }
                else
                {
                    foreach (var position in guess.NotPosPossitions)
                    {
                        wordList = wordList.Where(s => s[position] != guess.Character).ToList();
                    }
                }
            }


            Console.WriteLine($"Write letters not possible: (Current word count: {wordList.Count()})");
            var notPossibleLetters = Console.ReadLine();

            foreach (var letter in notPossibleLetters)
            {
                wordList = wordList.Where(s => !s.Contains(letter)).ToList();
            }

            Console.WriteLine($"possible options found: {wordList.Count()}");

            foreach (var word in wordList)
            {
                Console.WriteLine(word);
            }

            Console.ReadLine();
        }

        private static Letter ReadGuesses(int i)
        {
            Console.WriteLine($"Enter letter {i + 1}");
            char? character = ParseReadLineToChar(Console.ReadLine());

            if (character != null)
            {
                Console.WriteLine($"Enter position for letter {i + 1} (arrays start at 0)");
                string strPos = Console.ReadLine();
                int? position = null;
                List<int> notPosPositions = new List<int>();

                if (!string.IsNullOrEmpty(strPos))
                {
                    position = Convert.ToInt32(strPos);
                }
                else
                {
                    Console.WriteLine($"Enter positions letter {i + 1} cannot be (arrays start at 0, comma seperated)");
                    string strNotPos = Console.ReadLine();
                    var strList = strNotPos.Split(',').ToList();

                    foreach (var item in strList)
                    {
                        var num = Convert.ToInt32(item);
                        notPosPositions.Add(num);
                    }
                }

                return new Letter()
                {
                    Character = (char)character,
                    Position = position,
                    NotPosPossitions = notPosPositions
                };
            }
            return null;
        }

        public static char? ParseReadLineToChar(string response)
        {
            if (string.IsNullOrEmpty(response))
            {
                return null;
            }
            return response.ToLower()[0];
        }
    }
}
