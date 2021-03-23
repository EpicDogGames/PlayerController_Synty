using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG
{
    public class PlayerManager : MonoBehaviour
    {
        Animator animator;
        InputManager inputManager;
        CameraManager cameraManager;
        PlayerLocomotion playerLocomotion;

        public bool isInteracting;

        private void Awake() 
        {
            animator = GetComponent<Animator>();
            inputManager = GetComponentInParent<InputManager>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerLocomotion = GetComponentInParent<PlayerLocomotion>();
        }

        private void Update() 
        {
            inputManager.HandleAllInputs();
        }

        private void FixedUpdate() 
        {
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate() 
        {
            cameraManager.HandleAllCameraMovement();   

            isInteracting = animator.GetBool("isInteracting");
        }
    }
}