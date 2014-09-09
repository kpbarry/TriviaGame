using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame
{
    class Trivia
    {
        //TODO: Fill out the Trivia Object
        
        //The Trivia Object will have 2 properties
        // at a minimum, Question and Answer

        //The Constructor for the Trivia object will
        // accept only 1 string parameter.  You will
        // split the question from the answer in your
        // constructor and assign them to the Question
        // and Answer properties
        public string Question { get; set; }
        public string Answer { get; set; }
        //public string Category { get; set; }
        
        public Trivia(string splitting)
        {
            //Declare new list for splitting questions and answers
            List<string> split = new List<string>();
            //Split on *, index 0 is question, index 1 is answer
            split = splitting.Split('*').ToList();

            //Set index 0 to question
            this.Question = split[0];
            //Set index 1 to answer
            this.Answer = split[1];
        }

    }
}
