using System.Runtime.CompilerServices;
using System.Text;

[assembly:InternalsVisibleTo("MastermindTests")]
namespace MastermindGame
{
    public class Program {
        public static void Main(string[] args)
        {
            Mastermind mastermind = new();
            mastermind.PlayGame();
        }
    }


    public class Mastermind
    {

        //determines the length of the number
        internal const int MASTERMIND_DIGIT_LENGTH = 4;
        public List<int> correctDigits { get; private set; }
        /// <summary>
        /// Randomly sets the correct digits.
        /// </summary>
        public Mastermind()
        {
            correctDigits = GenerateCorrectDigits();
        }
        /// <summary>
        /// Manually sets the correct digits.
        /// </summary>
        internal Mastermind(List<int> correctDigits)
        {
            this.correctDigits = correctDigits;
        }

        /// <summary>
        /// Play the game
        /// </summary>
        public void PlayGame()
        {
            int numberOfGuesses = 0;
            Console.WriteLine("Welcome to Mastermind! You have 10 guesses to guess a four digit number with each digit between 1 and 6 inclusive. ");
            Console.WriteLine("After you make a guess each + will indicate you have a digit in the correct position and a - indicates a correct digit in the wrong place ");
            while (true)
            {
                Console.WriteLine("Please make a guess.");
                List<int> guessedDigits = ValidateAndConvertInputToDigits(Console.ReadLine());

                if (guessedDigits == null)
                {
                    Console.WriteLine("Invalid input. You need to enter four digits each between 1 and 6 inclusive.");
                    continue;
                }
                else
                {
                    //calculate 
                    string guessResultString = CalculateGuessAccuracy(guessedDigits);
                    numberOfGuesses++;
                    if (guessResultString == "++++") {
                        Console.WriteLine("Congratulations! You Win!");
                        return;
                    }
                    else if (numberOfGuesses >= 10)
                    {
                        Console.WriteLine("Sorry. You lose.");
                        Console.WriteLine($"Correct Answer: {string.Join(",", correctDigits)}");
                        return;
                    }
                    else
                    {
                        Console.WriteLine($"{guessResultString}. Guesses remaining: {10 - numberOfGuesses}");
                    }
                }
            }
        }

        /// <summary>
        /// Calculates how many digits are in the right place and how many right digits are in the wrong place.
        /// </summary>
        internal string CalculateGuessAccuracy(List<int> guessedDigits)
        {
            StringBuilder sb = new StringBuilder();
            List<int> nonExactDigits = new List<int>();
            //if the digit is correct, increment the exact digits, else add to a temp array of incorrect digits.
            for (int i = 0; i < guessedDigits.Count; i++)
            {
                if (correctDigits[i] == guessedDigits[i])
                    sb.Append('+');
                else
                    nonExactDigits.Add(correctDigits[i]);
            }
            //
            for (int i = 0; i < guessedDigits.Count; i++)
            {
                if (correctDigits[i] != guessedDigits[i] && nonExactDigits.Contains(guessedDigits[i]))
                {
                    sb.Append('-');
                    int idx = nonExactDigits.IndexOf(guessedDigits[i]);
                    nonExactDigits.RemoveAt(idx);
                }
            }
            return sb.ToString();
        }


        /// <summary>
        /// Converts the guess to an array of digits. Returns null if the guess is invalid.
        /// </summary>
        /// <param name="guess">A guess inputted by the user.</param>
        /// <returns>An array of the digits corresponsing to the user's guess. Returns null if guess is invalid.</returns>

        internal List<int> ValidateAndConvertInputToDigits(string? guess)
        {
            List<char> validDigits = ['1', '2', '3', '4', '5', '6'];
            try
            {

                //Don't parse if input is invlid. Invalid length, invalid characters etc.
                if (guess == null || guess.Length != MASTERMIND_DIGIT_LENGTH || (guess.Any(c => validDigits.Contains(c)) == false))
                    return null;
                return guess.Select(c => c - '0').ToList();

            }
            catch (Exception) {
                return null;
            }
        }

        
         /// <summary>
         /// Generates an array of four digits each being between 1 and 6. This is the correct guess.
         /// </summary>
         /// <returns></returns>
         
        public List<int> GenerateCorrectDigits()
        {
            List<int> digits = [];
            Random rand = new();
            for (int i = 0; i < MASTERMIND_DIGIT_LENGTH; i++)
            {
                digits.Add(rand.Next(1, 6));
            }
            return digits;
        }
    }
}