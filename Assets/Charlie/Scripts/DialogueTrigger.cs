using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject indicator;
    //the sprite that pops up when player gets in range
    [SerializeField] TextAsset inkJSON;
    //this is how it talks to the ink files

    private bool PlayerInRange;
   

    private void Awake()
    {
        //on the start of the game it makes the cue invisible
        PlayerInRange = false;
        indicator.SetActive(false);
    }

    private void Update()
    {
        if(PlayerInRange == true)
        {
            indicator.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            }

        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        //when player enters the trigger zone
        if (collider.gameObject.tag == "Player")
        {
            //for this to work the player object HAS to have the player tag
            PlayerInRange = true;
        }

    }

    private void OnTriggerExit(Collider collider)
    {
        // when the player leaves the trigger zone
        if (collider.gameObject.tag == "Player")
        {
            PlayerInRange = false;
        }
    }
}
