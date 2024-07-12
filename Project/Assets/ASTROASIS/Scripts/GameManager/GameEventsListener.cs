using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

namespace GameplayEvents
{
    [Serializable]
    public class GameEventsListener : MonoBehaviour
    {
        [SerializeField] 
        private List<GameEventListener> listeners;


        public void OnEnable()
        { 
            foreach (GameEventListener listener in listeners)
            {
                listener.Event.RegisterListener(this);
            }            
        }

        public void OnDisable()
        {
            foreach (GameEventListener listener in listeners)
            {
                listener.Event.UnregisterListener(this);
            }
        }

        public void OnEventRaised(GameEventSO gameEvent)
        {
            listeners.Find(x => x.Event == gameEvent).Response.Invoke();
        }

        [Serializable]
        private class GameEventListener
        {
            public GameEventSO Event;
            public UnityEvent  Response;
        }
    }
}

