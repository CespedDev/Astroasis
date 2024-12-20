using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PlayerSystem
{
    public class PlayerController : MonoBehaviour
    {
        /// Player's movement state
        [SerializeField] private PlayerStateSO State;

        /// Fordward direction vector
        [SerializeField] private Transform forward;

        private CharacterController characterController;
        private SmoothLocomotion    locomotion;


        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            locomotion          = GetComponent<SmoothLocomotion>();

            ResetState();
        }

        void Update()
        {
            if (State.ForwardMovement) 
                characterController.Move(Vector3.forward * State.ForwardSpeed * Time.deltaTime);
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
