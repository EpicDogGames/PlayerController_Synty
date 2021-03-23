using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG 
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        AnimatorManager animatorManager;
        PauseManager pauseManager;

        public Vector2 movementInput;
        public Vector2 cameraInput;

        public float cameraInputX;
        public float cameraInputY;

        public float verticalInput;
        public float horizontalInput;

        public float moveAmount;

        private void Awake() 
        {
            pauseManager = FindObjectOfType<PauseManager>();
            animatorManager = GetComponentInChildren<AnimatorManager>();   
        }

        private void OnEnable() 
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
                playerControls.PlayerAction.PauseGame.performed += _ => DeterminePauseStatus();
            }

            playerControls.Enable();
        }

        private void OnDisable() 
        {
            playerControls.Disable();    
        }

        public void SetTheAnimatorManager()
        {
            animatorManager = GetComponentInChildren<AnimatorManager>();         
        }
        
        public void HandleAllInputs()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

            cameraInputX = cameraInput.x;
            cameraInputY = cameraInput.y;

            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0, moveAmount);
        }

        private void DeterminePauseStatus()
        {
            if (pauseManager.PauseState())
            {
                pauseManager.ResumeGame();
            }
            else
            {
                pauseManager.PauseGame();
            }

        }
    }    
}
