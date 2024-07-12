using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerStateSO State;

        [SerializeField] private Transform forward;

        private CharacterController characterController;
        private SmoothLocomotion    locomotion;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            locomotion          = GetComponent<SmoothLocomotion>();

            ResetState();
        }

        void FixedUpdate()
        {
            if (State.ForwardMovement) 
                characterController.Move(Vector3.forward * State.ForwardSpeed);
        }

        public void ChangeState(PlayerStateSO state)
        {
            State = state;
            ResetState();
        }

        public void ResetPosition()
        {
            transform.position = new Vector3(0, transform.position.y + 1, 0);
        }

        private void ResetState()
        {
            locomotion.MovementSpeed     = State.MovementSpeed;
            locomotion.StrafeSpeed       = State.StrafeSpeed;
            locomotion.StrafeSprintSpeed = State.StrafeSprintSpeed;
            locomotion.ForwardDirection  = State.ForwardMovement ? forward : null;
        }
    }
}
