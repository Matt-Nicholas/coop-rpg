using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Interactable
{
    public int ID;


    public override void Interact(Player player)
    {
        base.Interact();

        player.EquipmentID = ID;

        GameObject.Destroy(gameObject);
    }
}
