using System;

namespace higherlower {
    class Card {
        int cardNumber = 0;
        int cardOld = 0;
        public void NewCard() {
            cardOld = cardNumber;
            Random rand = new Random();
            cardNumber = rand.Next(1, 13);
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
        public void Guess() {
            while(guess.ToLower() != "l" && guess.ToLower() != "h") {
                Console.Write("Higher or Lower [H/L]: ");
                guess = Console.ReadLine();
                if(guess.ToLower() != "l" && guess.ToLower() != "h") {
                    Console.WriteLine($"\"{guess}\" is not a valid response.");
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
            retry = Console.ReadLine();
            if(retry.ToLower() == "y") {
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
            while(a.GetPoints() > 0 && a.GetRetry().ToLower() == "y") {
                c.DisplayCard();
                c.NewCard();
                //Console.WriteLine($"OldCard: {c.GetCardOld()}\nNewCard: {c.GetCardNumber()}");
                a.Guess();
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