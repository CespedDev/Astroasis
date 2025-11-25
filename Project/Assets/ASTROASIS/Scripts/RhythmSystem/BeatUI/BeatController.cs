using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace RhythmSystem
{
    public class BeatController : MonoBehaviour
    {

        public int maxBeatLifeTime = 5;
        public int hotBeatPosition = 4;
        public AnimationCurve transparency = new AnimationCurve(new Keyframe(0, 0), new Keyframe(3, 5));

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
            GetComponent<Image>().color = new Color(1, 1, 1, transparency.Evaluate(beatLifeTime + RhythmManager.Instance.loopPositionInBeatsNormalize));
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
