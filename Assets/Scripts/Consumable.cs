using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Interactable
{
    public int value;

    public override void Interact(Player player)
    {
    
        player.Eat(value);
        Destroy(gameObject);
    }
}
