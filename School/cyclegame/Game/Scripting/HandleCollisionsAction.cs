using System;
using System.Collections.Generic;
using System.Data;
using Cyclegame.Game.Casting;
using Cyclegame.Game.Services;


namespace Cyclegame.Game.Scripting
{
    /// <summary>
    /// <para>An update action that handles interactions between the actors.</para>
    /// <para>
    /// The responsibility of HandleCollisionsAction is to handle the situation when the snake 
    /// collides with the food, or the snake collides with its segments, or the game is over.
    /// </para>
    /// </summary>
    public class HandleCollisionsAction : Action
    {
        private bool isGameOver = false;
        private bool p1win = false;
        private bool p2win = false;

        /// <summary>
        /// Constructs a new instance of HandleCollisionsAction.
        /// </summary>
        public HandleCollisionsAction()
        {
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            if (isGameOver == false)
            {
                HandleFoodCollisions(cast);
                HandleSegmentCollisions(cast);
                HandleGameOver(cast);
            }
        }

        /// <summary>
        /// Updates the score nd moves the food if the snake collides with it.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleFoodCollisions(Cast cast)
        {
            Player1 player1 = (Player1)cast.GetFirstActor("player1");
            Player2 player2 = (Player2)cast.GetFirstActor("player2");
            Score score = (Score)cast.GetFirstActor("score");
            Food food = (Food)cast.GetFirstActor("food");
            
            player1.GrowTail(1);
            player2.GrowTail(1);
            score.AddPoints(1);
        }

        /// <summary>
        /// Sets the game over flag if the snake collides with one of its segments.
        /// </summary>
        /// <param name="cast">The cast of actors.</param>
        private void HandleSegmentCollisions(Cast cast)
        {
            Player1 player1 = (Player1)cast.GetFirstActor("player1");
            Actor head1 = player1.GetHead();
            List<Actor> body1 = player1.GetBody();
            Player2 player2 = (Player2)cast.GetFirstActor("player2");
            Actor head2 = player2.GetHead();
            List<Actor> body2 = player2.GetBody();
            
            foreach (Actor segment in body1)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    p2win = true;
                }
                else if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    p1win = true;
                }
            }
            foreach (Actor segment in body2)
            {
                if (segment.GetPosition().Equals(head1.GetPosition()))
                {
                    isGameOver = true;
                    p2win = true;
                }
                else if (segment.GetPosition().Equals(head2.GetPosition()))
                {
                    isGameOver = true;
                    p1win = true;
                }
            }
        }

        private void HandleGameOver(Cast cast)
        {
            if (isGameOver == true)
            {
                Player1 player1 = (Player1)cast.GetFirstActor("player1");
                Player2 player2 = (Player2)cast.GetFirstActor("player2");
                List<Actor> segments1 = player1.GetSegments();
                List<Actor> segments2 = player2.GetSegments();
                List<Actor> body1 = player1.GetBody();
                List<Actor> body2 = player2.GetBody();
                Food food = (Food)cast.GetFirstActor("food");

                // create a "game over" message
                int x = Constants.MAX_X / 2;
                int y = Constants.MAX_Y / 2;
                Point position = new Point(x, y);

                Actor message = new Actor();
                if(p1win) {
                    message.SetText("Player 1 Wins!");
                }
                else if(p2win) {
                    message.SetText("Player 2 Wins!");
                }
                message.SetPosition(position);
                cast.AddActor("messages", message);

                // make everything white
                if(p1win) {
                    foreach (Actor segment in body1)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                    foreach (Actor segment in segments2)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                }
                else if(p2win) {
                    foreach (Actor segment in body2)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                    foreach (Actor segment in segments1)
                    {
                        segment.SetColor(Constants.WHITE);
                    }
                }
                food.SetColor(Constants.WHITE);
            }
        }

    }
}