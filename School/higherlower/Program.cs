using System;

namespace higherlower {
    class Card {
        int cardNumber = 0;
        int cardOld = 0;
        public void NewCard() {
            cardOld = cardNumber;
            Random rand = new Random();
            cardNumber = rand.Next(1, 14);
        }
        public void DisplayCard() {
            Console.WriteLine($"The card is [{cardNumber}]");
        }
        public void DisplayNext() {
            Console.WriteLine($"The next card was [{cardNumber}]");
        }
        public int GetCardNumber() {
            return cardNumber;
        }
        public int GetCardOld() {
            return cardOld;
        }
    }
    class Player {
        int points = 300;
        string guess = "";
        string retry = "y";
        public bool autoretry = false;
        public void Guess() {
            while(guess?.ToLower() != "l" && guess?.ToLower() != "h") {
                Console.Write("Higher or Lower [H/L]: ");
                guess = Console.ReadLine()!;
                if(guess?.ToLower() != "l" && guess?.ToLower() != "h") {
                    Console.WriteLine($"\"{guess}\" is not a valid response.");
                }
            }
        }
        public void Guess(Card card) {
            Console.Write("Higher or Lower [H/L]: ");
            if(card.GetCardOld() >= 8) {
                Thread.Sleep(500);
                Console.WriteLine("L");
                guess = "l";
            }
            else if(card.GetCardOld() <= 6) {
                Thread.Sleep(500);
                Console.WriteLine("H");
                guess = "h";
            }
            else if(card.GetCardOld() == 7) {
                Thread.Sleep(500);
                Random r = new Random();
                if(r.Next(0, 2) == 0) {
                    Console.WriteLine("L");
                    guess = "l";
                }
                else {
                    Console.WriteLine("H");
                    guess = "h";
                }
            }
        }
        public bool GuessTest(Card card) {
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
        public void ChangePoints(Card card) {
            if(GuessTest(card)) {
                points += 100;
            }
            else {
                points -= 75;
            }
        }
        public int GetPoints() {
            return points;
        }
        public bool KeepPlaying() {
            Console.WriteLine("Do you wish to keep playing [Y/N]? ");
            if(!autoretry) {
                retry = Console.ReadLine()!;
            }
            else {
                Thread.Sleep(2000);
                Console.WriteLine("Y");
                retry = "y";
            }
            if(retry?.ToLower() == "y") {
                return true;
            }
            else {
                Console.WriteLine("Thanks for playing!");
                return false;
            }
        }
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
