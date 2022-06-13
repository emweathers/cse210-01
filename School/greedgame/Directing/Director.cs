using System.Collections.Generic;
using greedgame.Casting;
using greedgame.Services;


namespace greedgame.Directing
{
    /// <summary>
    /// <para>A person who directs the game.</para>
    /// <para>
    /// The responsibility of a Director is to control the sequence of play.
    /// </para>
    /// </summary>
    public class Director
    {
        private KeyboardService keyboardService = null!;
        private VideoService videoService = null!;

        /// <summary>
        /// Constructs a new instance of Director using the given KeyboardService and VideoService.
        /// </summary>
        /// <param name="keyboardService">The given KeyboardService.</param>
        /// <param name="videoService">The given VideoService.</param>
        public Director(KeyboardService keyboardService, VideoService videoService)
        {
            this.keyboardService = keyboardService;
            this.videoService = videoService;
        }

        /// <summary>
        /// Starts the game by running the main game loop for the given cast.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void StartGame(Cast cast)
        {
            videoService.OpenWindow();
            while (videoService.IsWindowOpen())
            {
                GetInputs(cast);
                DoUpdates(cast);
                DoOutputs(cast);
            }
            videoService.CloseWindow();
        }

        /// <summary>
        /// Gets directional input from the keyboard and applies it to the player.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void GetInputs(Cast cast)
        {
            Actor player = cast.GetFirstActor("player");
            Point velocity = keyboardService.GetDirection();
            player.SetVelocity(velocity);     
        }

        /// <summary>
        /// Updates the player's and interactables' positions and resolves any collisions.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        private void DoUpdates(Cast cast)
        {
            Actor banner = cast.GetFirstActor("banner");
            Actor player = cast.GetFirstActor("player");
            List<Actor> interactables = cast.GetActors("interactables");
    
            banner.SetText("");
            int maxX = videoService.GetWidth();
            int maxY = videoService.GetHeight();
            player.MoveNext(maxX, maxY);
            string message = $"{player.points}";
            banner.SetText(message);
            if(player.points < 0 || greedgame.Program.dead) {
                Raylib_cs.Raylib.DrawText("YOU DIED", 325, 250, 50, new Raylib_cs.Color(255, 0, 0, 255));
                greedgame.Program.dead = true;
                Raylib_cs.Raylib.SetTargetFPS(0);
            }
            else {

                foreach (Actor actor in interactables)
                {
                    Random random = new Random();
                    if(random.Next(0, (2000+(int)(50*Raylib_cs.Raylib.GetTime()))) == 0) {
                        int x = random.Next(1, 60);
                        
                        Color color = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                        Point position = new Point(x, 0);
                        position = position.Scale(15);
                        Interactable gem = new Interactable();
                        gem.SetText("*");
                        gem.SetFontSize(15);
                        gem.SetColor(color);
                        gem.SetPosition(position);
                        gem.inflictingPoints = 20;
                        gem.ApplyPoints();
                        cast.AddActor("interactables", gem);
                    }
                    if(random.Next(0, 2000+(int)(50*Raylib_cs.Raylib.GetTime())) == 0) {
                        int x = random.Next(1, 60);

                        Color color = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                        Point position = new Point(x, 0);
                        position = position.Scale(15);
                        Interactable rock = new Interactable();
                        rock.SetText("o");
                        rock.SetFontSize(15);
                        rock.SetColor(color);
                        rock.SetPosition(position);
                        rock.inflictingPoints = -15;
                        rock.ApplyPoints();
                        cast.AddActor("interactables", rock);
                    }
                    actor.SetPosition(new Point(actor.GetPositionX(), actor.GetPositionY()+5));
                    if (player.GetPosition().Equals(actor.GetPosition()))
                    {
                        Interactable interactable = (Interactable) actor;
                        player.points += actor.points;
                        actor.SetColor(new Color(0,0,0));
                    }
                } 
            }
        }

        /// <summary>
        /// Draws the actors on the screen.
        /// </summary>
        /// <param name="cast">The given cast.</param>
        public void DoOutputs(Cast cast)
        {
            List<Actor> actors = cast.GetAllActors();
            videoService.ClearBuffer();
            videoService.DrawActors(actors);
            videoService.FlushBuffer();
        }
    }
}