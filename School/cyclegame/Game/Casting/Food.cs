using System;


namespace Cyclegame.Game.Casting
{
    /// <summary>
    /// <para>A tasty item that snakes like to eat.</para>
    /// <para>
    /// The responsibility of Food is to select a random position and points that it's worth.
    /// </para>
    /// </summary>
    public class Food : Actor
    {
        private int points = 0;

        /// <summary>
        /// Constructs a new instance of an Food.
        /// </summary>
        public Food()
        {
            SetText("@");
            SetColor(Constants.RED); 
            Reset();
        }

        /// <summary>
        /// Gets the points this food is worth.
        /// </summary>
        /// <returns>The points.</returns>
        public int GetPoints()
        {
            return points;
        }

        /// <summary>
        /// Selects a random position and points that the food is worth.
        /// </summary>
        public void Reset()
        {
            points = 1;
            int x = 0;
            int y = 0;
            Point position = new Point(x, y);
            position = position.Scale(Constants.CELL_SIZE);
            SetPosition(position);
        }
    }
}