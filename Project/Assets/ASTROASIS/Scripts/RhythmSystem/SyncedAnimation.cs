using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SyncedAnimation : MonoBehaviour
{
    /// <summary>
    /// The animator controller attached to this GameObject.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Records the animation state or animation that the Animator is currently in.
    /// </summary>
    private AnimatorStateInfo animatorStateInfo;

    /// <summary>
    /// Used to address the current state within the Animator using the Play() function.
    /// </summary>
    public int currentState;

    void Start()
    {
        //Load the animator attached to this object
        animator = GetComponent<Animator>();

        //Get the info about the current animator state
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        //Convert the current state name to an integer hash for identification
        currentState = animatorStateInfo.fullPathHash;
    }

    void Update()
    {
        // CHANGE THIS !!!!!!!!!!!!!
        //Start playing the current animation from wherever the current conductor loop is
        //animator.Play(currentState, -1, (RhythmSystem.RhythmManager.instance.loopPositionInBeatsNormalize); 

        //Set the speed to 0 so it will only change frames when you next update it
        animator.speed = 0;
    }
}
