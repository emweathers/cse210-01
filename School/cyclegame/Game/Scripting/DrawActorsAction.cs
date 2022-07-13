using System.Collections.Generic;
using Cyclegame.Game.Casting;
using Cyclegame.Game.Services;


namespace Cyclegame.Game.Scripting
{
    /// <summary>
    /// <para>An output action that draws all the actors.</para>
    /// <para>The responsibility of DrawActorsAction is to draw each of the actors.</para>
    /// </summary>
    public class DrawActorsAction : Action
    {
        private VideoService videoService;

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public DrawActorsAction(VideoService videoService)
        {
            this.videoService = videoService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            Player1 player1 = (Player1)cast.GetFirstActor("player1");
            Player2 player2 = (Player2)cast.GetFirstActor("player2");
            List<Actor> segments1 = player1.GetSegments();
            List<Actor> segments2 = player2.GetSegments();
            //Actor score = cast.GetFirstActor("score");
            //Actor food = cast.GetFirstActor("food");
            List<Actor> messages = cast.GetActors("messages");
            
            videoService.ClearBuffer();
            videoService.DrawActors(segments1);
            videoService.DrawActors(segments2);
            //videoService.DrawActor(score);
            //videoService.DrawActor(food);
            videoService.DrawActors(messages);
            videoService.FlushBuffer();
        }
    }
}