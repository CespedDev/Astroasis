// Copyright (c) Carlos Cabrera 06/09/2023

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace RhythmSystem
{
    public class RhythmManager : MonoBehaviour
    {
        public static RhythmManager Instance { get; private set; }

        [field: Header("Song parameters")]

        /// <summary>
        /// Song beats per minute. This is determined by the song you're trying to sync up to.
        /// </summary>
        [field: SerializeField] public float songBpm { get; private set; }
        
        /// <summary>
        /// The offset to the first beat of the song in seconds.
        /// </summary>
        [field: SerializeField] public float firstBeatOffset { get; private set; }
        
        /// <summary>
        /// An AudioSource attached to this GameObject that will play the music.
        /// </summary>
        [field: SerializeField] public AudioSource musicSource { get; private set; }

        /// <summary>
        /// The number of beats in each loop.
        /// </summary>
        [field: SerializeField] public float beatsPerLoop { get; private set; }

        [field: Header("Timed hit settings")]

        /// <summary>
        /// All accuracy bonuses. Is important to maintain better to worst order.
        /// </summary>
        [SerializeField] private List<RhythmBonusSO> accuracyBonuses;


        // CONDUCTOR DATA -------------------------------------------
        public float secPerBeat                     { get; private set; }
        public float songPosition                   { get; private set; }
        public float songPositionInBeats            { get; private set; }

        [field: SerializeField] public float dspSongTime                    { get; private set; }
        public int   completedLoops                 { get; private set; } = 0;
        [field: SerializeField] public float loopPositionInBeats;
        [field: SerializeField] public float loopPositionInBeatsNormalize   { get; private set; }

        // RHYTHM CHECKED
        public RhythmBonusSO lastRhythm { get; private set; }

        private bool startedRhythm = false;

        // Beat tracking
        private int lastBeatIndex = -1;

        // EVENTS
        /// <summary>
        /// Event triggered every time a new beat occurs. Passes the current beat index.
        /// </summary>
        public event Action<int> OnBeat;

        void Awake()
        {
            // SINGLETON control
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;

            musicSource = GetComponent<AudioSource>();
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null; 
        }

        void Start()
        {
            //Calculate the number of seconds in each beat
            secPerBeat = 60f / songBpm / musicSource.pitch;

            startedRhythm = true;

            dspSongTime = (float)AudioSettings.dspTime;

            //Start the music
            musicSource.Play();
        }

        void Update()
        {
            if (!startedRhythm) return;

            //Determine how many seconds since the song started
            songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);

            //Determine how many beats since the song started
            songPositionInBeats = songPosition / secPerBeat;

            // Check if we've hit a new beat
            int currentBeatIndex = Mathf.FloorToInt(songPositionInBeats);
            if (currentBeatIndex != lastBeatIndex && currentBeatIndex >= 0)
            {
                lastBeatIndex = currentBeatIndex;
                OnBeat?.Invoke(currentBeatIndex);
            }

            //Calculate the loop position
            if (songPositionInBeats >= (completedLoops + 1) * beatsPerLoop)
                completedLoops++;
            loopPositionInBeats = songPositionInBeats - completedLoops * beatsPerLoop;

            loopPositionInBeatsNormalize = loopPositionInBeats - Mathf.Floor(loopPositionInBeats);
        }

        /// <summary>
        /// Check the bonus should be apply
        /// </summary>
        /// <param name="rhythmChecker"></param>
        public void CheckRhythmBonus(out RhythmBonusSO rhythmBonus)
        {
            lastRhythm = null;

            //Calculate the normalize loop position
            loopPositionInBeatsNormalize = loopPositionInBeats - Mathf.Floor(loopPositionInBeats);

            foreach (RhythmBonusSO bonus in accuracyBonuses)
            {
                Debug.Log($"{bonus.color}: {bonus.BeatAccuracy} of {loopPositionInBeatsNormalize}");

                if (loopPositionInBeatsNormalize >= 1 - bonus.BeatAccuracy ||
                    loopPositionInBeatsNormalize <= bonus.BeatAccuracy)
                {
                    lastRhythm = bonus;
                    break;
                }
            }

            rhythmBonus = lastRhythm;
        }

        public void StartRhythm()
        {
            

            

            
        }
    }
}
