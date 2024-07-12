using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayEvents
{
    [CreateAssetMenu(fileName = "GameEventSO", menuName = "Scriptable/Game Event")]
    public class GameEventSO : ScriptableObject
    {
        private List<GameEventsListener> listeners =
             new List<GameEventsListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                listeners[i].OnEventRaised(this);
            }
        }

        public void RegisterListener(GameEventsListener listener)
        { listeners.Add(listener); }
        public void UnregisterListener(GameEventsListener listener)
        { listeners.Remove(listener); }
    }
}
