using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            //The logic for your trivia game happens here
            List<Trivia> AllQuestions = GetTriviaList();
            //Greet user with a separate function
            Greet();

            //Set up variables for trivia
            int correct = 0;
            int incorrect = 0;
            int score = 0;

            //Give user 10 wrong guesses
            while (incorrect < 10)
            {
                //Random number generator to get question at random
                Random rng = new Random();
                int randomNum = rng.Next(1, 5001);

                //Set a variable called triviaQuestion to the question string
                var triviaQuestion = GetTriviaList()[randomNum];

                //Trivia.Question will break up the string into a question and 
                //display that to the user
                Console.WriteLine(triviaQuestion.Question);
                //Take in user input
                Console.Write("Your answer: ");
                string input = Console.ReadLine();

                //Convert triviaQuestion.Answer to lowercase and check if the answer is correct
                if (input == triviaQuestion.Answer.ToLower())
                {
                    correct++;
                    score += 100;
                    Console.Clear();
                    Console.WriteLine("Good guess!");
                }
                //Wrong answer, tell user answer
                else
                {
                    incorrect++;
                    score -= 50;
                    Console.Clear();
                    Console.WriteLine(input + " is wrong!\nThe right answer is: " + triviaQuestion.Answer + "\n");
                }
                //Print correct and score
                Console.WriteLine("Correct: " + correct + "\nScore: " + score + "\nYou have " + (10 - incorrect) + " incorrect guesses remaining.\n");
            }
            AddHighScore(correct);
            DisplayHighScores();
            Console.ReadKey();
        }

        static void AddHighScore(int playerScore)
        {
            Console.WriteLine("Add your name to high scores list: ");
            string playerName = Console.ReadLine();

            //Create a gateway to the database
            KevinEntities db = new KevinEntities();

            //Create new high score object
            HighScore newHighScore = new HighScore();
            newHighScore.DateCreated = DateTime.Now;
            newHighScore.Game = "Trivia";
            newHighScore.Name = playerName;
            newHighScore.Score = playerScore;

            //Add it to the database
            db.HighScores.Add(newHighScore);

            //Save changes to db
            db.SaveChanges();
        }

        static void DisplayHighScores()
        {
            Console.Clear();
            Console.WriteLine("Trivia High Scores");
            Console.WriteLine("-----------------------------");

            KevinEntities db = new KevinEntities();
            List<HighScore> highScoreList = db.HighScores.Where(x => x.Game == "Trivia").OrderBy(x => x.Score).Take(10).ToList();
            foreach (HighScore i in highScoreList)
            {
                Console.WriteLine("{0}. {1} - {2} - {3}", highScoreList.IndexOf(i) + 1, i.Name, i.Score, i.DateCreated.Value.ToShortDateString());
            }
        }

        //This functions gets the full list of trivia questions from the Trivia.txt document
        static List<Trivia> GetTriviaList()
        {
            //Get Contents from the file.  Remove the special char "\r".  Split on each line.  Convert to a list.
            List<string> contents = File.ReadAllText("trivia.txt").Replace("\r", "").Split('\n').ToList();

            //Each item in list "contents" is now one line of the Trivia.txt document.

            //make a new list to return all trivia questions
            List<Trivia> returnList = new List<Trivia>();
            // TODO: go through each line in contents of the trivia file and make a trivia object.
            //       add it to our return list.
            // Example: Trivia newTrivia = new Trivia("what is my name?*question");
            //Go through every string (QandA) in contents
            foreach (string s in contents)
            {
                //Declare newTrivia for every string in contents, separated into question and answer
                Trivia newTrivia = new Trivia(s);
                returnList.Add(newTrivia);
            }
            //Return the full list of trivia questions
            return returnList;
        }

        static void Greet()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("Hello, " + name + " you will answer questions until you get 10 wrong or hit Ctrl+C.");
        }
    }
}
