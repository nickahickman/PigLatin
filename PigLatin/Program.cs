using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Linq;

namespace PigLatin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("PigLatin translator");

            while (true)
            {
                string userString = GetUserResponse("What would you like to translate?");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(TranslateUserInput(userString));
                Console.ResetColor();
                Console.WriteLine("========================================================");

                if (!UserWantsToContinue())
                {
                    Console.WriteLine("Thanks for playing! See you next time.");
                    break;
                }
            }
        }
        public static string GetUserResponse(string prompt)
        {
            string userResponse;

            Console.WriteLine(prompt);
            userResponse = Console.ReadLine();

            while (String.IsNullOrEmpty(userResponse))
            {
                Console.WriteLine($"I need you to enter something. Silence isn't a virtue here!");
                userResponse = GetUserResponse(prompt);
            }

            return userResponse;
        }
        public static string TranslateUserInput(string s)
        {
            string workingString = s.ToLower();

            if (ContainsSpecialCharacters(workingString) || ContainsNumbers(workingString))
            {
                return workingString;
            }
            else if (!IsMoreThanOneWord(s))
            {
                return TranslateSingleWord(workingString);
            }
            else
            {
                return TranslateSentence(workingString);
            }
        }
        public static bool ContainsSpecialCharacters(string s)
        {
            string allowedCharacters = "',.!?";

            foreach (char c in s)
            {
                if (allowedCharacters.Contains(c))
                {
                    continue;
                }
                else if (!Char.IsLetterOrDigit(c) && !Char.IsWhiteSpace(c))
                {
                    return true;
                }
            }

            return false;
        }
        public static bool IsMoreThanOneWord(string s)
        {
            return s.Split(" ").Length > 1;
        }
        public static string TranslateSingleWord(string word)
        {
            int firstVowelIndex = FindIndexOfFirstVowel(word);

            if (EndsWithPunctuation(word))
            {
                string punctuation = GetPunctuation(word);
                word = TrimEndingPunctuation(word);

                if (firstVowelIndex <= 0)
                {
                    return $"{word}way{punctuation}";
                }
                else
                {
                    return $"{TranslateWordBeginningWithConsonant(word, firstVowelIndex)}{punctuation}";
                }
            }
            else
            {
                if (firstVowelIndex <= 0)
                {
                    return $"{word}way";
                }
                else
                {
                    return TranslateWordBeginningWithConsonant(word, firstVowelIndex);
                }
            }
        }
        public static string TranslateSentence(string sentence)
        {
            Array words = sentence.Split();
            string translatedSentence = "";

            foreach (string word in words)
            {
                translatedSentence += $"{TranslateSingleWord(word)} ";
            }

            return translatedSentence.Trim();
        }
        public static string TranslateWordBeginningWithConsonant(string s, int firstVowelIndex)
        {
            string substringBeforeFirstVowel = s.Substring(0, firstVowelIndex);
            string substringWithFirstVowel = s.Substring(firstVowelIndex);

            return $"{substringWithFirstVowel}{substringBeforeFirstVowel}ay";
        }
        public static bool EndsWithPunctuation(string s)
        {
            string punctuationChars = ".,!?";

            foreach (char c in punctuationChars)
            {
                if (s.EndsWith(c))
                {
                    return true;
                }
            }
            return false;
        }
        public static string TrimEndingPunctuation(string s)
        {
            char[] punctuationChars = { '.', ',', '!', '?' };
            return s.TrimEnd(punctuationChars);
        }
        public static string GetPunctuation(string s)
        {
            int stringLength = s.Length;

            return s.Substring(stringLength - 1);
        }
        public static bool ContainsNumbers(string s)
        {
            foreach (char c in s)
            {
                if (Char.IsDigit(c))
                {
                    return true;
                }
            }

            return false;
        }
        public static int FindIndexOfFirstVowel(string s)
        {
            char[] vowels = { 'a', 'e', 'i', 'o', 'u' };
            return s.IndexOfAny(vowels, 0);
        }
        public static bool UserWantsToContinue()
        {
            string userResponse = GetUserResponse("Should we translate something else? (y/n)").ToLower(); ;

            while (userResponse != "n" && userResponse != "y")
            {
                userResponse = GetUserResponse("Sorry, I didn't understand that. Should we translate something? (y/n)").ToLower();
            }

            if (userResponse == "n")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
