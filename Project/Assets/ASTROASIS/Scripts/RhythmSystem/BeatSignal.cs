using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Pool;

namespace RhythmSystem
{
    public class BeatSignal : MonoBehaviour
    {

        public RectTransform hotBeat;
        public RectTransform spawn;
        public GameObject beatPrefab;

        private float startPosition;

        private ObjectPool beatLeftPool = new ObjectPool(beatPrefab, 5);

        void Awake()
        {

        }

        void Update()
        {
            if (RhythmManager.Instance != null)
                spawn.localPosition = new Vector3((startPosition + hotBeat.localPosition.x) * (1- RhythmManager.Instance.loopPositionInBeatsNormalize), spawn.localPosition.y, spawn.localPosition.z);
        }
    }
}