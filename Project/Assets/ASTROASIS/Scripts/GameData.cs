using UnityEngine;
using UnityEngine.Events;

namespace GameplayData
{
    public class GameData
    {
        /// <summary>
        /// Player score
        /// </summary>
        public float score = 0;

        /// <summary>
        /// Score bonus multiplier
        /// </summary>
        public float bonus = 1;

        /// <summary>
        /// Targets killed during the game.
        /// </summary>
        public float targetsKilled = 0;
    }
}