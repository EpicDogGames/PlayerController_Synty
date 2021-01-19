using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BG
{
    public class UIController : MonoBehaviour
    {
        public GameObject pausePanel_FirstButton;
        [SerializeField] GameObject pausePanel;        

        public void TogglePauseOn()
        {
            pausePanel.SetActive(true);

            // clear the selected object
            EventSystem.current.SetSelectedGameObject(null);
            // set the new selected object
            EventSystem.current.SetSelectedGameObject(pausePanel_FirstButton);
        }

        public void TogglePauseOff()
        {
            pausePanel.SetActive(false);
        }
    }    
}
