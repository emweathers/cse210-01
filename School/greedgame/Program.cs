using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using greedgame.Casting;
using greedgame.Directing;
using greedgame.Services;

namespace greedgame
{
    /// <summary>
    /// The program's entry point.
    /// </summary>
    class Program
    {
        public static bool dead = false;
        private static int FRAME_RATE = 12;
        private static int MAX_X = 900;
        private static int MAX_Y = 600;
        private static int CELL_SIZE = 15;
        private static int FONT_SIZE = 15;
        private static int COLS = 60;
        private static int ROWS = 40;
        private static string CAPTION = "Greed Game";
        private static Color WHITE = new Color(255, 255, 255);
        private static int DEFAULT_ARTIFACTS = 5;

        /// <summary>Used for making letters</summary>
        static void LetterMaker(string Letter, Random random, Cast cast, int xPos) {
            Interactable interactable = new Interactable();
            interactable.SetText(Letter);
            interactable.SetFontSize(FONT_SIZE*2);
            interactable.SetColor(new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256)));
            interactable.SetPosition(new Point(xPos, 5).Scale(CELL_SIZE*2));
            cast.AddActor("interactables", interactable);
        }

        /// <summary>
        /// Starts the program using the given arguments.
        /// </summary>
        /// <param name="args">The given arguments.</param>
        static void Main(string[] args)
        {
            // create the cast
            Cast cast = new Cast();

            // create the banner
            Actor banner = new Actor();
            banner.SetText("");
            banner.SetFontSize(FONT_SIZE);
            banner.SetColor(WHITE);
            banner.SetPosition(new Point(CELL_SIZE, 0));
            cast.AddActor("banner", banner);

            // create the player
            Actor player = new Actor();
            player.SetText("#");
            player.SetFontSize(FONT_SIZE);
            player.SetColor(WHITE);
            player.SetPosition(new Point(MAX_X / 2, MAX_Y-25));
            cast.AddActor("player", player);

            // create the interactables
            Random random = new Random();
            for (int i = 0; i < DEFAULT_ARTIFACTS; i++)
            {
                LetterMaker("G", random, cast, 10);
                LetterMaker("R", random, cast, 11);
                LetterMaker("E", random, cast, 12);
                LetterMaker("E", random, cast, 13);
                LetterMaker("D", random, cast, 14);
                
                LetterMaker("G", random, cast, 16);
                LetterMaker("A", random, cast, 17);
                LetterMaker("M", random, cast, 18);
                LetterMaker("E", random, cast, 19);
            }

            // start the game
            KeyboardService keyboardService = new KeyboardService(CELL_SIZE);
            VideoService videoService = new VideoService(CAPTION, MAX_X, MAX_Y, CELL_SIZE, FRAME_RATE, false);
            Director director = new Director(keyboardService, videoService);
            director.StartGame(cast);
        }
    }
}