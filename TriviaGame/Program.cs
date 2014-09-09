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

            Greet();

            //Set up variables for trivia
            int correct = 0;
            int incorrect = 0;
            int score = 0;

            while (incorrect < 10)
            {
                Random rng = new Random();
                int randomNum = rng.Next(1, 5001);

                var triv = GetTriviaList()[randomNum];

                Console.WriteLine(triv.Question);
                Console.WriteLine("Your answer: ");
                string input = Console.ReadLine();

                if (input == triv.Answer.ToLower())
                {
                    correct++;
                    score += 100;
                    Console.WriteLine("Good guess!");
                }
                else if ((triv.Answer).Contains(input))
                {
                    correct++;
                    score += 50;
                    Console.WriteLine("Kinda right");
                }
                else
                {
                    incorrect++;
                    Console.WriteLine("WRONG! The right answer was: " + triv.Answer);
                }
            }
            Console.WriteLine("Score: " + score);
            Console.ReadKey();
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
                //Declare newTrivia for every QandA string in contents
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
            Console.WriteLine("Hello, " + name + " you will answer questions until you get five wrong or hit Ctrl+C.");
        }
    }
}
