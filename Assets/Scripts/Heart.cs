using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Interactable
{
    public override void Interact(Player player)
    {
        player.AddHeart();
        GameObject.Destroy(this.gameObject);
    }
}
