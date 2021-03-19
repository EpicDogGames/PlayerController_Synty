using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BG
{
    public class CharacterManager : MonoBehaviour
    {
        public GameObject[] characters;

        public void SwitchCharacters(int index)
        {
            for (int i=0; i<characters.Length; i++)
            {
                characters[i].SetActive(false);

            }

            characters[index].SetActive(true);
            characters[index].GetComponentInParent<InputManager>().SetTheAnimatorManager();
        }
    }
}