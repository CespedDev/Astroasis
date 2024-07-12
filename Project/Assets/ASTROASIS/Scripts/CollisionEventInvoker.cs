using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionEventInvoker : MonoBehaviour
{
    [SerializeField]
    private List<TaggedEvent> taggedEvents;

    private void OnCollisionEnter(Collision collision)
    {
        taggedEvents.Find(x => x.tag == collision.transform.tag).OnCollision.Invoke();

        //Invoke
    }

    [Serializable]
    private class TaggedEvent
    {
        public string tag = "Untagged";
        public UnityEvent OnCollision;
    }
}
