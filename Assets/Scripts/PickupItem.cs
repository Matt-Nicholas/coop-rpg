using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : Interactable
{

    public override void Interact(Player playerNumber)
    {
        //InventoryController.Instance.GiveItem(ItemDrop);
        Destroy(gameObject);
    }
}
