using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BG 
{
    public class PauseManager : MonoBehaviour
    {
        public GameObject pausePanel_FirstButton;
        [SerializeField] GameObject pausePanel;

        public bool paused = false;

        public bool PauseState()
        {
            return paused;
        }

        public void PauseGame()
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            paused = true;

            // clear the selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set the new selected object
            EventSystem.current.SetSelectedGameObject(pausePanel_FirstButton);

        }

        public void ResumeGame()
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }    
}
