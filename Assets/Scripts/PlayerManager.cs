using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG
{
    public class PlayerManager : MonoBehaviour
    {
        InputManager inputManager;
        PlayerLocomotion playerLocomotion;

        private void Awake() 
        {
            inputManager = GetComponent<InputManager>();
            playerLocomotion = GetComponent<PlayerLocomotion>();
        }

        private void Update() 
        {
            inputManager.HandleAllInputs();
            inputManager.HandleTogglePause();
        }

        private void FixedUpdate() 
        {
            playerLocomotion.HandleAllMovement();
        }

        private void LateUpdate() 
        {
            inputManager.togglePause_Input = false;    
        }
    }
}