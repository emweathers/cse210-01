using System;

namespace jumpergame {
    class Player {
        public int attempts = 4;
        public string? guessedLetter;
        public char[] guessedWord = {'_','_','_','_','_','_','_','_'};
        public string? guessWordStr;
        public string Guess() {
            Console.Write("Guess a letter [a-z]: ");
            guessedLetter = Console.ReadLine()!.ToUpper();
            return guessedLetter;
        }
    }

    class Word {
        Random rand = new Random();
        string[] words = {"ABSOLUTE","COMPRISE","EPIPHANY","BACTERIA","YOURSELF","KEYBOARD","TANGIBLE","VOLATILE","SYMBOLIC","UNLIKELY"};
        public string selectedWord = "";
        public Word() {
            selectedWord = words[rand.Next(0,5)];
        }
    }

    class Validator {
        public void Check(Player player, Word word) {
            bool isCorrect = false;
            Console.WriteLine("");
            player.Guess();
            for(int i = 0; i < 8; i++) {
                if(player.guessedLetter == word.selectedWord[i].ToString()) {
                    player.guessedWord[i] = word.selectedWord[i];
                    player.guessWordStr = new string(player.guessedWord);
                    isCorrect = true;
                }
            }
            Console.WriteLine(player.guessedWord);
            if(isCorrect == false) {
                player.attempts -= 1;
            }
            else {
                isCorrect = false;
            }
        }
    }

    class Graphics {
        public void Update(Player player) {
            if(player.attempts == 4) {
                Console.WriteLine("  ___ \n /___\\\n \\   /\n  \\ /\n   O  \n  /|\\ \n  / \\\n\n^^^^^^^");
            }
            else if(player.attempts == 3) {
                Console.WriteLine("\n /___\\\n \\   /\n  \\ /\n   O  \n  /|\\ \n  / \\\n\n^^^^^^^");
            }
            else if(player.attempts == 2) {
                Console.WriteLine("\n \\   /\n  \\ /\n   O  \n  /|\\ \n  / \\\n\n^^^^^^^");
            }
            else if(player.attempts == 1) {
                Console.WriteLine("\n  \\ /\n   O  \n  /|\\ \n  / \\\n\n^^^^^^^");
            }
            else if(player.attempts == 0) {
                Console.WriteLine("\n   x  \n  /|\\ \n  / \\\n^^^^^^^");
            }
        }
    }

    class Program {
        static void Main() {
            Player player = new Player();
            Word word = new Word();
            Validator validator = new Validator();
            Graphics graphics = new Graphics();
            //Console.WriteLine(word.selectedWord);
            Console.WriteLine(player.guessedWord);
            while(player.attempts > 0 && player.guessWordStr != word.selectedWord) {
                graphics.Update(player);
                validator.Check(player, word);
            }
            if(player.attempts == 0) {
                graphics.Update(player);
                Console.WriteLine("You have failed!");
            }
            else {
                graphics.Update(player);
                Console.WriteLine("You have succeeded!");
            }
        }
    }
}
