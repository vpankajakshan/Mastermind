using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Mastermind
{
    class Program
    {
        const int CODE_LENGTH = 4;
        const int NUM_ATTEMPTS = 10;

        static void Main(string[] args)
        {
            int[] code = SetCodeChallenge();
            // Console.WriteLine("code {0}:", string.Join("", code));
            PrintResult(CodeCrackerPlay(code), code);
            Console.ReadKey();
        }

        static void PrintResult(bool result, int[] code)
        {
            Console.WriteLine();
            Console.WriteLine("===========================");

            if (result)
            { 
                Console.WriteLine("You've cracked the code!!");
            }
            else
            {
                Console.WriteLine("You've lost the game :(");
            }

            
            Console.WriteLine("Code maker's code: {0}", string.Join("", code));
            Console.WriteLine("===========================");
        }

        static bool CodeCrackerPlay(int[] code)
        {
            int i = 0;
            while (i < NUM_ATTEMPTS)
            {
                Console.WriteLine();
                Console.Write("Enter your guess #{0}: ", i + 1);
                string userInput = Console.ReadLine().Trim();

                if (ValidateInput(userInput))
                {
                    // Converting user input to integer array
                    int[] guess = userInput.ToCharArray().Select(x => (int)Char.GetNumericValue(x)).ToArray();

                    //checking for similarties
                    Tuple<int, string> feedback = CodeCheck(code, guess);
                    Console.WriteLine("Feedback: {0}", feedback.Item2);

                    if (feedback.Item1 == CODE_LENGTH) // Item1 is the number of +ve cases found
                    {
                        return true;
                    }
                    i++;
                }
            }
            return false;
        }

        static bool ValidateInput(string userInput)
        {
            try
            {
                int.Parse(userInput);
            }
            catch (FormatException)
            {
                Console.WriteLine("ERROR: Enter a numeric input");
                return false;
            }

            if (userInput.Length != CODE_LENGTH)
            {
                Console.WriteLine("ERROR: Enter a {0} digit numeric input", CODE_LENGTH);
                return false;
            }
            return true;
        }

        static int[] SetCodeChallenge()
        {
            var code = new int[CODE_LENGTH];
            Random rnd = new Random();

            for (int i = 0; i < CODE_LENGTH; i++)
            {
                code[i] = rnd.Next(1, 7);
            }

            return code;
        }

        static Tuple<int, string> CodeCheck(int[] code, int[] guess)
        {
            /*
             * The time Complexity of this function is O(N).. N being code length
             * with the worst case space complexity of O(N) for hashset
             */
            var uniqueCodes = new HashSet<int>(code);
            var sbPlus = new StringBuilder();
            var sbMinus = new StringBuilder();


            for (int i=0; i < CODE_LENGTH; i++)
            {
                if (guess[i] == code[i])
                {
                    sbPlus.Append('+');
                }
                else if (uniqueCodes.Contains(guess[i]))
                {
                    sbMinus.Append('-');
                }
            }
            var strFeedback = string.Concat(sbPlus.ToString(), sbMinus.ToString());
            return Tuple.Create(sbPlus.Length, strFeedback);
        }
    }
}
