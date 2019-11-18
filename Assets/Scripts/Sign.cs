using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    [SerializeField] string title;
    [SerializeField] string[] dialogue;

    public override void Interact(Player player)
    {
        DialogueManager.Instance.AddDialogue(title, dialogue);
    }

}
