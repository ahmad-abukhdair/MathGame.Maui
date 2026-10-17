using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Math_Game
{
    public class MathGameApp
    {
        private readonly Random _random = new Random();
        private readonly List<GameResult> _gameHistory = new();
        const int questionCount = 5;
        public void Run()
        {
            

            while (true)
            {
                DisplayMenu();

                if (!RuntheGame())
                {
                    return;
                }





            }
        }
        private bool RuntheGame()
        {
            int choice = ReadNumber("Choose an option: ");
            if (choice == 0)
            {
                return false;
            }

            if (choice < 1 || choice > 5)
            {
                Console.WriteLine("Please choose a number from 0 to 4.");

                return true;
            }
            if (choice == 5) { DisplayGameHistory(); return true; }

            MathOperation operation = choice switch
            {
                1 => MathOperation.Addition,
                2 => MathOperation.Subtraction,
                3 => MathOperation.Multiplication,
                4 => MathOperation.Division,
                _ => throw new ArgumentOutOfRangeException(nameof(choice))
            };
            int score = 0;

            for (int questionNumber = 1; questionNumber <= questionCount; questionNumber++)
            {
                MathQuestion question = GenerateQuestion(operation);

                Console.WriteLine($"\nQuestion {questionNumber}/{questionCount}");
                Console.WriteLine(question.GetQuestionText());

                int answer = ReadNumber("Your answer: ");

                if (question.IsCorrect(answer))
                {
                    score++;
                    Console.WriteLine("Correct!");
                }
                else
                {
                    Console.WriteLine("Incorrect.");
                }
            }

            Console.WriteLine($"\nYour score: {score}/{questionCount}\n");

            GameResult result = new GameResult(operation, score, questionCount); _gameHistory.Add(result);
            return true;
        }
      
            private void DisplayGameHistory()
            {
                Console.WriteLine("\n===== Game History =====");

                if (_gameHistory.Count == 0)
                {
                    Console.WriteLine("No games played yet.");
                    return;
                }

                for (int i = 0; i < _gameHistory.Count; i++)
                {
                    GameResult result = _gameHistory[i];

                    Console.WriteLine(
                        $"{i + 1}. Operation: {result.Operation} | " +
                        $"Score: {result.Score}/{result.TotalQuestions}");
                }

                Console.WriteLine();
            }


        private void DisplayMenu()
        {
            Console.WriteLine("===== Math Game =====");
            Console.WriteLine("1. Addition");
            Console.WriteLine("2. Subtraction");
            Console.WriteLine("3. Multiplication");
            Console.WriteLine("4. Division");
            Console.WriteLine("5. View Game History");
            Console.WriteLine("0. Exit");
        }

        private int ReadNumber(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int number))
                {
                    return number;
                }

                Console.WriteLine("Invalid input. Please enter a whole number.");
            }
        }
        private MathQuestion GenerateQuestion(MathOperation operation)
        {
            int firstNumber;
            int secondNumber;

           

            if (operation == MathOperation.Division)
            {
                // المقسوم عليه: من 1 إلى 100، فلا يكون صفرًا.

                int divisor = _random.Next(1, 100);

                // أكبر ناتج يضمن ألا يتجاوز المقسوم 100.

                int maxQuotient = 100 / divisor;
                // ناتج صحيح، يشمل الصفر والحد الأعلى.

                int quotient = _random.Next(0, maxQuotient + 1);

                firstNumber = divisor * quotient;
                secondNumber = divisor;
            }
            else
            {
                firstNumber = _random.Next(0, 101);
                secondNumber = _random.Next(0, 101);
            }
            return new MathQuestion( firstNumber, secondNumber, operation);
        }

    }
}
