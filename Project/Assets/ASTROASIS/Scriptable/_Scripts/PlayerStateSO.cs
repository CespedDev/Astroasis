using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerSystem
{
    [CreateAssetMenu(fileName = "PlayerStateSO", menuName = "Scriptable/Player State")]
    public class PlayerStateSO : ScriptableObject
    {
        public float MovementSpeed;

        public float StrafeSpeed;

        public float StrafeSprintSpeed;

        public bool  ForwardMovement;

        public float ForwardSpeed;
    }
}