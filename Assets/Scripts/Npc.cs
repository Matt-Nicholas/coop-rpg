using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : Interactable
{
    [SerializeField] string title;
    //[SerializeField] public List<List<string>> dialogues;
    [SerializeField] public List<string[]> dialogues;

    private void Start()
    {
        //dialogues = new List<List<string>>()
        dialogues = new List<string[]>()
        {
            {new string[3]{ "Hold it right there children.", "It's to dangerous to go out there with out a weapon.", "Come back when you are better prepared."}},
            {new string[2]{ "Still no sword eh?", "Try looking around the forest to the west of here."}},
            {new string[4]{ "Hello again children.", "Whats that? You got a sword! Wow!!", "I'm afraid it's still to dangerous out there for you.", "Come back when you have more stamina."}},
            {new string[4]{ "Oh hello children. I see you have everything!", "I'm afraid I still cant let you pass", "Because this world happens to be flat and" +
            " Matt didnt build anymore map.", "So if you go any further you will fall off the edge of the world."}}
        };
    }

    public override void Interact(Player player)
    {
        if(player.EquipmentID == 0)
        {
            if (!hasInteracted)
            {
                // Base
                DialogueManager.Instance.AddDialogue(title, dialogues[0]);
            }
            else
            {
                // Still base
                DialogueManager.Instance.AddDialogue(title, dialogues[1]);
            }
                
        }
        else if(player.EquipmentID == 1)
        {
             if(player.HealthUnlocked <= 12)
            {
                // has sword
                DialogueManager.Instance.AddDialogue(title, dialogues[2]);
            }
            else
            {
                //has sword and heart
                DialogueManager.Instance.AddDialogue(title, dialogues[3]);
            }
        }

        hasInteracted = true;
    }

    public class Dialogue
    {
        [SerializeField]
        private string title;
        [SerializeField]
        private List<string[]> dialogues = new List<string[]>();
    }
}

