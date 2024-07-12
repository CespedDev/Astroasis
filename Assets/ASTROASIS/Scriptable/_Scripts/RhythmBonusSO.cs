using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RhythmSystem
{
    [CreateAssetMenu(fileName = "RhythmBonusSO", menuName = "Scriptable/Rhythm Bonus")]
    public class RhythmBonusSO : ScriptableObject
    {
        /// <summary>
        /// Bonus value that will multiply to the damage
        /// </summary>
        public float Bonus = 1;

        /// <summary>
        /// Accuracy needed to perform a hit of this type,
        /// being 0 the max accuracy and 0.5f no accuracy
        /// </summary>
        [Range(0f, 0.5f)]
        public float BeatAccuracy = 0;

        /// <summary>
        /// Bonus text color. Used on debug texts, like 
        /// in dummies.
        /// </summary>
        public Color color = Color.white;

        // Posibility of add other reference like hit sound

    }
}
