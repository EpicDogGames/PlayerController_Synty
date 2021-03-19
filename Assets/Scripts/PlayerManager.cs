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
            animator = GetComponentInChildren<Animator>();
            inputManager = GetComponent<InputManager>();
            cameraManager = FindObjectOfType<CameraManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
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
            Debug.Log("Is interacting state: " + isInteracting);
        }
    }
}