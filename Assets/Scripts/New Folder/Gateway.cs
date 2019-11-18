using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateway : Interactable
{
    [SerializeField]
    private string _targetSceneName;
    [SerializeField]
    private int targetGateID;


    public override void Interact(Player player)
    {
        // if use gate returns true, we are entering a gate. Not exiting
        player.playerController.UseGate(_targetSceneName, targetGateID, true);
        
        
    }
}
