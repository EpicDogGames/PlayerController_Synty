using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG 
{
    public class PlayerLocomotion : MonoBehaviour
    {
        PlayerManager playerManager;
        AnimatorManager animatorManager;
        InputManager inputManager;

        Vector3 moveDirection;
        Transform cameraObject;
        Rigidbody playerRigidbody;

        [Header("Falling")]
        public float inAirTimer;
        public float leapingVelocity;
        public float fallingVelocity;
        public float rayCastHeightOffset = 0.5f;
        public LayerMask groundLayer;                   // only detect things marked as ground layer (any model set with default layer setting)

        [Header("Movement Flags")]
        public bool isGrounded;

        [Header("Movement Speeds")]
        public float walkingSpeed = 1.5f;
        public float runningSpeed = 5.0f;
        public float sprintingSpeed = 7.0f;
        public float rotationSpeed = 15;

        private void Awake() 
        {
            playerManager = GetComponent<PlayerManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();
            inputManager = GetComponent<InputManager>();
            playerRigidbody = GetComponent<Rigidbody>();
            cameraObject = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleFallingAndLanding();

            // if isInteracting is true, don't want player to move or rotate so just return from the method
            if (playerManager.isInteracting)
            {
                Debug.Log("Locomotion: " + playerManager.isInteracting);
                return;
            }

            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            moveDirection = cameraObject.forward * inputManager.verticalInput;
            moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
            moveDirection.Normalize();
            moveDirection.y = 0;

            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
            Vector3 movementVelocity = moveDirection;
            playerRigidbody.velocity = movementVelocity;
        }

        private void HandleRotation()
        {
            Vector3 targetDirection = Vector3.zero;

            targetDirection = cameraObject.forward * inputManager.verticalInput;
            targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
            targetDirection.Normalize();
            targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            transform.rotation = playerRotation;
        }

        private void HandleFallingAndLanding()
        {
            RaycastHit hit;
            Vector3 rayCastOrigin = transform.position;                 // starts raycast at the bottom of player's feet
            rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;    // make sure raycast starts above the ground so sphere can detect what is at player's feet

            if (!isGrounded)
            {
                if (!playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation("Falling", true);
                }

                inAirTimer = inAirTimer + Time.deltaTime;
                playerRigidbody.AddForce(transform.forward * leapingVelocity);      // applies a leap out to motion instead of just falling off
                playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);  // longer in air, quicker you will fall
            }

            // create an invisible sphere around the raycast origin (feet of player)
            if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
            {
                // player is hitting ground, so switch to land animation
                if (!isGrounded && !playerManager.isInteracting)
                {
                    animatorManager.PlayTargetAnimation("Land", true);
                }
                inAirTimer = 0;
                isGrounded = true;
            }
            else
            {
                // player still is in the air
                isGrounded = false;
            }
        }
    }
    
}
