using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG 
{
    public class InputManager : MonoBehaviour
    {
        PlayerControls playerControls;
        AnimatorManager animatorManager;
        UIController uiController;

        public Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;

        private float moveAmount;

        public bool togglePause_Input;
        public bool togglePauseFlag;

        private void Awake() 
        {
            uiController = FindObjectOfType<UIController>(); 
            animatorManager = GetComponentInChildren<AnimatorManager>();   
        }

        private void OnEnable() 
        {
            if (playerControls == null)
            {
                playerControls = new PlayerControls();

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
                playerControls.PlayerAction.TogglePause.performed += i => togglePause_Input = true;
            }

            playerControls.Enable();
        }

        private void OnDisable() 
        {
            playerControls.Disable();    
        }

        public void HandleAllInputs()
        {
            HandleMovementInput();
        }

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
            animatorManager.UpdateAnimatorValues(0, moveAmount);
        }

        public void HandleTogglePause()
        {
            if (togglePause_Input)
            {
                togglePauseFlag = !togglePauseFlag;

                if (togglePauseFlag)
                {
                    uiController.TogglePauseOn();
                }
                else
                {
                    uiController.TogglePauseOff();
                }
            }
        }
    }    
}
