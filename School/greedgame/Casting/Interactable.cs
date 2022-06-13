namespace greedgame.Casting
{
    /// <summary>
    /// <para>A rock or a gem.</para>
    /// <para>
    /// The responsibility of an Interactable is to inflict points.
    /// </para>
    /// </summary>
    public class Interactable : Actor
    {
        public int inflictingPoints;

        /// <summary>Applies inflictingPoints onto the inherited member, points</summary>
        // Only really good for better organization
        public void ApplyPoints() {
            points = inflictingPoints;
        }
    }
}