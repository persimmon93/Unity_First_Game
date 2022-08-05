using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

/// <summary>
/// Script that handles all interactable objects.
/// </summary>
/// 

public class Interactable : MonoBehaviour
{
    //Distance the player has to be with the object to interact.
    public float radius = 1.5f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;

    bool hasInteracted = false;
    
    //Virtual is a method that is meant to be overwritten.
    public virtual void Interact()
    {
        //This method is meant to be overwritten.
        //Debug.Log("Interacting with " + transform.name);
    }
    void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
            interactionTransform = transform;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }
}
