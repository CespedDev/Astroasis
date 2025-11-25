using BNG;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace RhythmSystem
{
    public class BeatSignalUI : MonoBehaviour
    {
        public RectTransform leftSpawn;
        public RectTransform leftTarget;
        public RectTransform rightSpawn;
        public RectTransform rightTarget;
        public float targetOffset = 0f;

        public BeatController beatPrefab;
        public bool collectionChecks = true;
        public int maxPoolSize = 16;

        private IObjectPool<BeatController> beatPool;

        public IObjectPool<BeatController> Pool
        {
            get
            {
                if (beatPool == null)
                {
                    beatPool = new ObjectPool<BeatController>(
                        CreateBeat,
                        OnTakeFromPool,
                        OnReturnedToPool,
                        OnDestroyBeat,
                        collectionChecks,
                        14,
                        maxPoolSize
                    );
                }

                return beatPool;
            }
        }

        BeatController CreateBeat()
        {
            Debug.Log("Creating new BeatController for pool.");
            BeatController beat = Instantiate(beatPrefab, transform);
            beat.pool = Pool;
            return beat;
        }

        void OnTakeFromPool(BeatController beat)
        {
            beat.gameObject.SetActive(true);
        }

        void OnReturnedToPool(BeatController beat)
        {
            beat.gameObject.SetActive(false);
        }

        void OnDestroyBeat(BeatController beat)
        {
            Destroy(beat.gameObject);
        }

        public void SpawnBeat()
        {
            BeatController b = Pool.Get();

            b.transform.position = transform.position;
            b.transform.rotation = transform.rotation;
        }

        private void Start()
        {
            Debug.Log(RhythmManager.Instance);
            if (RhythmManager.Instance != null)
            {
                RhythmManager.Instance.OnBeat += HandleBeat;
            }
        }

        private void HandleBeat(int beatIndex)
        {
            // LEFT SPAWN
            BeatController beat = Pool.Get();
            beat.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -90f));
            beat.spawnTransform = leftSpawn;
            beat.targetTransform = leftTarget;
            beat.targetOffset = targetOffset;

            // RIGHT SPAWN
            beat = Pool.Get();
            beat.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90f));
            beat.spawnTransform = rightSpawn;
            beat.targetTransform = rightTarget;
            beat.targetOffset = -targetOffset;
        }
    }
}