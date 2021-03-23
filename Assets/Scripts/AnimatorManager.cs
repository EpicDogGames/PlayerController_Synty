using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG
{
    public class AnimatorManager : MonoBehaviour
    {
        Animator animator;

        int horizontal;
        int vertical;

        private void Awake() 
        {
            animator = GetComponent<Animator>();
            horizontal = Animator.StringToHash("Horizontal");
            vertical = Animator.StringToHash("Vertical");
        }

        public void PlayTargetAnimation(string targetAnimation, bool isInteracting)
        {
            // if isInteracting is true, player can't move or rotate while the animation is running
            animator.SetBool("isInteracting", isInteracting);
            animator.CrossFade(targetAnimation, 0.2f);
        }

        public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
        {
            // animation snapping
            float snappedHorizontal;
            float snappedVertical;

            #region horizontal snapping
            if (horizontalMovement > 0 && horizontalMovement < 0.55f)
            {
                snappedHorizontal = 0.5f;
            }
            else if (horizontalMovement > 0.55f)
            { 
                snappedHorizontal = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f)
            {
                snappedHorizontal = -0.5f;
            }
            else if (horizontalMovement < -0.55f)
            {
                snappedHorizontal = -1;
            }
            else
            {
                snappedHorizontal = 0;
            }
            #endregion

            #region vertical snapping
            if (verticalMovement > 0 && verticalMovement < 0.55f)
            {
                snappedVertical = 0.5f;
            }
            else if (verticalMovement > 0.55f)
            { 
                snappedVertical = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f)
            {
                snappedVertical = -0.5f;
            }
            else if (verticalMovement < -0.55f)
            {
                snappedVertical = -1;
            }
            else
            {
                snappedVertical = 0;
            }
            #endregion

            animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
            animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        }
    }
}