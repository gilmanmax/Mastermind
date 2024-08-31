using MastermindGame;

namespace MastermindTests
{
    [TestClass]
    public class MastermindTests
    {
        /// <summary>
        /// Tests to see four digits are generated
        /// </summary>
        [TestMethod]
        public void TestRandomGeneratorDigitIsFourDigits()
        {
            Mastermind mastermind = new();
            int lengthOfDigits = mastermind.GenerateCorrectDigits().Count;
            Assert.AreEqual(Mastermind.MASTERMIND_DIGIT_LENGTH, lengthOfDigits);
        }

        /// <summary>
        /// Tests to see if the digits generated are valid
        /// </summary>
        [TestMethod]
        public void TestRandomDigitsAreValid()
        {
            List<int> validDigits = new List<int> { 1, 2, 3, 4, 5, 6 };
            Mastermind mastermind = new();
            List<int> digits = mastermind.GenerateCorrectDigits();
            foreach (int digit in validDigits)
            {
                Assert.IsTrue(validDigits.Contains(digit));
            }

        }
        /// <summary>
        /// Tests to see if a null guess is not valid.
        /// Though you shouldn't be able to from a ReadLine() anyway.
        /// </summary>
        [TestMethod]
        public void TestNullGuessIsInvalid()
        {
            Mastermind mastermind = new();
            var convertedToDigits = mastermind.ValidateAndConvertInputToDigits(null);
            Assert.IsNull(convertedToDigits, $"Not null: value is {string.Join(',', convertedToDigits)}");
        }
        #region Edge Cases
        /// <summary>
        /// Right answer is 1122, you guess 1112. Should return +++.
        /// Edge case is because the third one is a duplicate and the program could mark it as a "-" for being in spots 1 and 2.
        /// +++- should never be a thing!!
        /// </summary>
        [TestMethod]
        public void TestDuplicateDigitsShouldBeWrongDigits()
        {
            List<int> correctDigits = new() { 1, 1, 2, 2 };
            List<int> playerGuess = new() { 1, 1, 1, 2 };
            Mastermind mastermind = new Mastermind(correctDigits);
            string guessAccuracy = mastermind.CalculateGuessAccuracy(playerGuess);
            Assert.AreEqual(guessAccuracy, "+++");
        }

        /// <summary>
        /// Right answer is 2345, you guess 5432. Should return ----.
        /// </summary>
        [TestMethod]
        public void TestAllFourDigitsAreInWrongPlace()
        {
            List<int> correctDigits = new() { 2, 3, 4, 5 };
            List<int> playerGuess = new() { 5, 4, 3, 2 };
            Mastermind mastermind = new Mastermind(correctDigits);
            string guessAccuracy = mastermind.CalculateGuessAccuracy(playerGuess);
            Assert.AreEqual(guessAccuracy, "----");
        }
        /// <summary>
        /// Right answer is 1632, you guess 1111. Should return +.
        /// </summary>
        [TestMethod]
        public void TestThreeDupes()
        {
            List<int> correctDigits = new() { 1, 6, 3, 2 };
            List<int> playerGuess = new() { 1, 1, 1, 1 };
            Mastermind mastermind = new Mastermind(correctDigits);
            string guessAccuracy = mastermind.CalculateGuessAccuracy(playerGuess);
            Assert.AreEqual(guessAccuracy, "+");
        }

        /// <summary>
        /// Right answer is 2233, you guess 2323. Should return ++--.
        /// </summary>
        [TestMethod]
        public void TestTwoRightTwoInWrongPlace()
        {
            List<int> correctDigits = new() { 2, 2, 3, 3 };
            List<int> playerGuess = new() { 2, 3, 2, 3 };
            Mastermind mastermind = new Mastermind(correctDigits);
            string guessAccuracy = mastermind.CalculateGuessAccuracy(playerGuess);
            Assert.AreEqual(guessAccuracy, "++--");
        }


        #endregion
    }
}