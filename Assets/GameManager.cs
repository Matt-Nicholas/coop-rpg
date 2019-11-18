using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Player[] players;
    [SerializeField]
    private string _firstScene = "";

    void Start()
    {
        players = GameObject.FindObjectsOfType<Player>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            if (InputManager.AButton(1) || InputManager.AButton(2))
            {
                if(players.Length > 0)
                {
                    if (SceneManager.GetSceneByName(_firstScene) != null)
                        players[0].playerController.UseGate(_firstScene, -1, false);
                    else
                        Debug.LogWarning($"{ _firstScene } not found. Unable to load \"first scnene\"");
                }
            }
        }
    }

    void OnGUI()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            GUI.TextField(new Rect(Screen.width / 2 - 40, 600, 80, 30), "Press A");
        }
    }
}
