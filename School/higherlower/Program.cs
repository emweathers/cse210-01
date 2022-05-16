using System;

namespace higherlower {
    class Card {
        int cardNumber = 0;
        int cardOld = 0;
        ///<summary>Converts cardOld to the current cardNumber and then changes cardNumber to a random number between 1 and 13</summary>
        public void NewCard() {
            cardOld = cardNumber;
            Random rand = new Random();
            cardNumber = rand.Next(1, 14);
        }
        ///<summary>Displays the current card</summary>
        public void DisplayCard() {
            Console.WriteLine($"The card is [{cardNumber}]");
        }
        ///<summary>Displays the current card, with a different text format</summary>
        public void DisplayNext() {
            Console.WriteLine($"The next card was [{cardNumber}]");
        }
        ///<summary>Gets the value of cardNumber</summary>
        public int GetCardNumber() {
            return cardNumber;
        }
        ///<summary>Gets the value of cardOld</summary>
        public int GetCardOld() {
            return cardOld;
        }
    }
    class Player {
        int points = 300;
        string guess = "";
        string retry = "y";
        public bool autoretry = false;
        ///<summary>Allows the user to make a guess</summary>
        public void Guess() {
            while(guess?.ToLower() != "l" && guess?.ToLower() != "h") {
                Console.Write("Higher or Lower [H/L]: ");
                guess = Console.ReadLine()!;
                if(guess?.ToLower() != "l" && guess?.ToLower() != "h") {
                    Console.WriteLine($"\"{guess}\" is not a valid response.");
                }
            }
        }
        ///<summary>Automates the guessing process</summary>
        public void Guess(Card card) {
            Console.Write("Higher or Lower [H/L]: ");
            if(card.GetCardOld() >= 8) {
                Thread.Sleep(250);
                Console.WriteLine("L");
                guess = "l";
                Thread.Sleep(250);
            }
            else if(card.GetCardOld() <= 6) {
                Thread.Sleep(250);
                Console.WriteLine("H");
                guess = "h";
                Thread.Sleep(250);
            }
            else if(card.GetCardOld() == 7) {
                Thread.Sleep(250);
                Random r = new Random();
                if(r.Next(0, 2) == 0) {
                    Console.WriteLine("L");
                    guess = "l";
                }
                else {
                    Console.WriteLine("H");
                    guess = "h";
                }
                Thread.Sleep(250);
            }
        }
        ///<summary>Checks if the guess is correct in relation to the current cardNumber</summary>
        private bool GuessTest(Card card) {
            if((card.GetCardOld() <= card.GetCardNumber()) && (guess.ToLower() == "h")) {
                guess = "";
                return true;
            }
            else if((card.GetCardOld() >= card.GetCardNumber()) && (guess.ToLower() == "l")) {
                guess = "";
                return true;
            }
            else {
                guess = "";
                return false;
            }
        }
        ///<summary>Adds or removes the points in relation to the guess from GuessTest()</summary>
        public void ChangePoints(Card card) {
            if(GuessTest(card)) {
                points += 100;
            }
            else {
                points -= 75;
            }
        }
        ///<summary>Gets the current player score</summary>
        public int GetPoints() {
            return points;
        }
        ///<summary>Gives prompt to whether the player wishes to keep playing or not</summary>
        public bool KeepPlaying() {
            Console.WriteLine("Do you wish to keep playing [Y/N]? ");
            if(!autoretry) {
                retry = Console.ReadLine()!;
            }
            else {
                Thread.Sleep(1000);
                Console.WriteLine("Y");
                retry = "y";
                Thread.Sleep(1000);
            }
            if(retry?.ToLower() == "y") {
                return true;
            }
            else {
                Console.WriteLine("Thanks for playing!");
                return false;
            }
        }
        ///<summary>Gets whether the player wishes to continue playing</summary>
        public string GetRetry() {
            return retry;
        }
    }
    class Program {
        static void Main() {
            Player a = new Player();
            Card c = new Card();
            c.NewCard();
            //a.autoretry = true; // Comment this line out if you do not wish to automate the process, or uncomment it if you want it to be automated
            while(a.GetPoints() > 0 && a.GetRetry().ToLower() == "y") {
                c.DisplayCard();
                c.NewCard();
                //Console.WriteLine($"OldCard: {c.GetCardOld()}\nNewCard: {c.GetCardNumber()}");
                if(a.autoretry) {
                    a.Guess(c);
                }
                else {
                    a.Guess();
                }
                c.DisplayNext();
                a.ChangePoints(c);
                Console.WriteLine(a.GetPoints());
                if(a.GetPoints() > 0) {
                    a.KeepPlaying();
                }
            }
            if(a.GetPoints() <= 0) {
                Console.WriteLine("Too bad, you lost.");
            }
        }
    }
}
