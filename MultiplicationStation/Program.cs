using System;
using System.Diagnostics;

class MathStation
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int score = 0;
        int questionsAsked = 0;
        bool keepPlaying = true;

        Stopwatch stopwatch = new Stopwatch();
        TimeSpan totalTime = new TimeSpan();

        Console.WriteLine("Select Difficulty Level: 1-Easy, 2-Medium, 3-Hard");
        int difficulty = Convert.ToInt32(Console.ReadLine());

        int maxNumber = difficulty switch
        {
            1 => 10, // Easy: Numbers between 1 and 10
            2 => 20, // Medium: Numbers between 1 and 20
            3 => 50, // Hard: Numbers between 1 and 50
            _ => 10, // Default to Easy
        };

        // Set different time limits for each difficulty level
        int timeLimit = difficulty switch
        {
            1 => 10000, // Easy: 10 seconds
            2 => 7000,  // Medium: 7 seconds
            3 => 5000,  // Hard: 5 seconds
            _ => 10000, // Default to Easy
        };

        Console.WriteLine("Select operations to include (e.g., 1 2 3 4 for all): 1-Addition, 2-Subtraction, 3-Multiplication, 4-Division");
        string[] selectedOperations = Console.ReadLine().Split();

        while (keepPlaying)
        {
            // Select a random operation from the chosen ones
            int operation = Convert.ToInt32(selectedOperations[random.Next(selectedOperations.Length)]);

            int num1 = random.Next(1, maxNumber + 1);
            int num2 = random.Next(1, maxNumber + 1);
            int correctAnswer = 0;
            string question = "";

            switch (operation)
            {
                case 1: // Addition
                    correctAnswer = num1 + num2;
                    question = $"{num1} + {num2}";
                    break;
                case 2: // Subtraction
                    correctAnswer = num1 - num2;
                    question = $"{num1} - {num2}";
                    break;
                case 3: // Multiplication
                    correctAnswer = num1 * num2;
                    question = $"{num1} * {num2}";
                    break;
                case 4: // Division
                    // Ensure division results in a whole number
                    while (num2 == 0 || num1 % num2 != 0)
                    {
                        num1 = random.Next(1, maxNumber + 1);
                        num2 = random.Next(1, maxNumber + 1);
                    }
                    correctAnswer = num1 / num2;
                    question = $"{num1} / {num2}";
                    break;
            }

            Console.WriteLine($"What is {question}?");

            stopwatch.Start();
            int userAnswer = Convert.ToInt32(Console.ReadLine());
            stopwatch.Stop();

            TimeSpan timeTaken = stopwatch.Elapsed;
            totalTime += timeTaken;
            questionsAsked++;
            stopwatch.Reset();

            if (userAnswer == correctAnswer)
            {
                Console.WriteLine($"Correct! Time taken: {timeTaken.TotalSeconds} seconds.");
                score++;
            }
            else
            {
                Console.WriteLine($"Incorrect! The correct answer is {correctAnswer}. Time taken: {timeTaken.TotalSeconds} seconds.");
            }

            Console.WriteLine("Play again? (yes/y/no/n)");
            string userResponse = Console.ReadLine().Trim().ToLower();
            string[] acceptedYesAnswers = { "yes", "y" };
            keepPlaying = Array.Exists(acceptedYesAnswers, answer => answer == userResponse);
        }

        if (questionsAsked > 0)
        {
            double averageTime = totalTime.TotalSeconds / questionsAsked;
            Console.WriteLine($"Game over. Your final score is: {score}. Average time per question: {averageTime:F2} seconds.");
        }
        else
        {
            Console.WriteLine($"Game over. Your final score is: {score}. No questions were answered.");
        }
    }
}