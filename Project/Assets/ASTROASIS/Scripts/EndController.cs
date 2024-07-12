using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndController : MonoBehaviour
{
    public UnityEvent EndActivated;
    private bool activated;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !activated)
        {
            EndActivated.Invoke();
            activated = true;
        }
            
    }
}
