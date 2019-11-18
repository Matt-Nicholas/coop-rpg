using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public bool hasInteracted;

    private void Awake()
    {
        transform.tag = "Interactable";
    }

    public virtual void Interact()
    {
        hasInteracted = true;
    }
    public virtual void Interact(Player playerNumber)
    {
        Interact();
    }

    public virtual void Interact(PlayerController pc)
    {
        Interact();
    }
}
