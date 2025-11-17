using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;

namespace RhythmSystem
{
    public class BeatController : MonoBehaviour
    {

        public int maxBeatLifeTime = 5;
        public int hotBeatPosition = 4;
        public AnimationCurve transparency;

        [NonSerialized] public float targetOffset = 0f;
        [NonSerialized] public Transform spawnTransform;
        [NonSerialized] public Transform targetTransform;
        public IObjectPool<BeatController> pool;

        private int beatLifeTime = 0;

        private void OnEnable()
        {
            beatLifeTime = 0;
            if (RhythmManager.Instance != null)
            {
                RhythmManager.Instance.OnBeat += HandleBeat;
            }
        }

        private void OnDisable()
        {
            if (RhythmManager.Instance != null)
            {
                RhythmManager.Instance.OnBeat -= HandleBeat;
            }
        }

        void Update()
        {
            float beatPositionX = ((beatLifeTime + RhythmManager.Instance.loopPositionInBeatsNormalize) * (targetTransform.localPosition.x + targetOffset - spawnTransform.localPosition.x) / maxBeatLifeTime) + spawnTransform.localPosition.x;
            transform.localPosition = new Vector3(beatPositionX, transform.localPosition.y, transform.localPosition.z);
        }

        private void HandleBeat(int beatIndex)
        {
            beatLifeTime++;
            if (beatLifeTime >= maxBeatLifeTime)
            {
                beatLifeTime = 0;
                pool.Release(this);
            }
        }
    }
}
