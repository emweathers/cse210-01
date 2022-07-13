using Cyclegame.Game.Casting;
using Cyclegame.Game.Services;


namespace Cyclegame.Game.Scripting
{
    /// <summary>
    /// <para>An input action that controls the snake.</para>
    /// <para>
    /// The responsibility of ControlActorsAction is to get the direction and move the snake's head.
    /// </para>
    /// </summary>
    public class ControlActorsAction : Action
    {
        private KeyboardService keyboardService;
        private Point direction1 = new Point(0, -1 * Constants.CELL_SIZE);
        private Point direction2 = new Point(0, -1 * Constants.CELL_SIZE);

        /// <summary>
        /// Constructs a new instance of ControlActorsAction using the given KeyboardService.
        /// </summary>
        public ControlActorsAction(KeyboardService keyboardService)
        {
            this.keyboardService = keyboardService;
        }

        /// <inheritdoc/>
        public void Execute(Cast cast, Script script)
        {
            // left
            if (keyboardService.IsKeyDown("a")) {
                direction1 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (keyboardService.IsKeyDown("d")) {
                direction1 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (keyboardService.IsKeyDown("w")) {
                direction1 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (keyboardService.IsKeyDown("s")) {
                direction1 = new Point(0, Constants.CELL_SIZE);
            }

            Player1 player1 = (Player1)cast.GetFirstActor("player1");
            player1.TurnHead(direction1);

            // left
            if (keyboardService.IsKeyDown("j")) {
                direction2 = new Point(-Constants.CELL_SIZE, 0);
            }

            // right
            if (keyboardService.IsKeyDown("l")) {
                direction2 = new Point(Constants.CELL_SIZE, 0);
            }

            // up
            if (keyboardService.IsKeyDown("i")) {
                direction2 = new Point(0, -Constants.CELL_SIZE);
            }

            // down
            if (keyboardService.IsKeyDown("k")) {
                direction2 = new Point(0, Constants.CELL_SIZE);
            }

            Player2 player2 = (Player2)cast.GetFirstActor("player2");
            player2.TurnHead(direction2);

        }
    }
}