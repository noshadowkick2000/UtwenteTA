using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PromptScript : MonoBehaviour
{
    [SerializeField] private GameObject player; //Player GameObject, used to get position
    
    private Player _player; //Player script, used to display interaction (i.e. dialogue and progress)

    //GameObject that displays the text
    [SerializeField]private GameObject interactionText;

        //private GameObject[] interactionTextChildren;

    public bool interactionApproached;
 
    // How far you can interact with the object from
    private float interactionDistance = 5f;
    int currentInteractionText = 0;
 
    void Start () {
     
        //turns off the text (just in case it wasn't already
        //interactionText.SetActive(false);

        //interactionTextChildren = new GameObject[interactionText.transform.childCount];

        for (int i = 0; i < interactionText.transform.childCount; i++)
        {
            //turns off the text (just in case it wasn't already)
            interactionText.transform.GetChild(i).gameObject.SetActive(false);
            
        }
        
        Debug.Log("interactionText.transform.childCount" + interactionText.transform.childCount);
        
    }

    private void Update()
    {
        //Ray of player position and direction they look at
        Ray ray = new Ray(player.transform.position, player.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            
            
            if (hit.collider.tag.Contains("Interactable"))
            {
                for (int i = 0; i < interactionText.transform.childCount; i++)
                {
                    //check to find which interaction text matches the gameobject
                    if (interactionText.transform.GetChild(i).transform.name.Contains(gameObject.transform.name))
                    {
                        currentInteractionText = i;
                        //Debug.Log("true");
                        break;
                    }
                    Debug.Log("false");
                }
                
                //Debug.Log("currentInteractionText " + currentInteractionText);
                //Debug.Log("currentInteractionText " + currentInteractionText);
                
                //Interaction Prompt display
                interactionText.transform.GetChild(currentInteractionText).gameObject.SetActive(true); // Turns on the interaction prompt.
                Renderer interactionTextRenderer = interactionText.transform.GetChild(currentInteractionText).GetComponent<Renderer>();
                Vector3 interactionTextOffset = new Vector3(0, 0.5f, (interactionTextRenderer.bounds.size.magnitude) / 2); //set offset of the prompt (z: based on size)
                interactionText.transform.GetChild(currentInteractionText).transform.position = gameObject.transform.position + interactionTextOffset; //set the position of the interaction text to be above the interactable object
                interactionText.transform.GetChild(currentInteractionText).transform.rotation = Quaternion.LookRotation(interactionText.transform.position-player.transform.position); //rotate the text toward the camera

                interactionApproached = true;
                
                

                //Interaction with Keypress 'e'
                if (Input.GetKeyDown((KeyCode) 'e'))
                {

                    if (hit.collider.tag.Contains("Progress"))
                    {
                        //Trigger progress based on the collided object
                        //Debug.Log("Progress");

                    }
                    else
                    {
                        //Trigger a dialogue based on the the collided object
                        //Debug.Log("Dialogue");

                        //_player.Dialogue(gameObject);

                        //Destroy(hit.transform.gameObject);
                    }
                    
                    //Debug.Log("No action");
                }

            }
            else
            {

                // Turns the prompt back off when you're not looking at the object.
                interactionText.transform.GetChild(currentInteractionText).gameObject.SetActive(false);

                interactionApproached = false;
            }
        }

    }


}
