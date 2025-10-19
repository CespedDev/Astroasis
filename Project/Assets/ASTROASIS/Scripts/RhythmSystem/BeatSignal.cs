using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmSystem
{
    public class BeatSignal : MonoBehaviour
    {

        public RectTransform hotBeat;
        public RectTransform beat;

        private float startPosition;

        void Awake()
        {
            startPosition = beat.localPosition.x;
        }

        void Update()
        {
            if (RhythmManager.Instance != null)
                beat.localPosition = new Vector3((startPosition + hotBeat.localPosition.x) * (1- RhythmManager.Instance.loopPositionInBeatsNormalize), beat.localPosition.y, beat.localPosition.z);
        }
    }
}