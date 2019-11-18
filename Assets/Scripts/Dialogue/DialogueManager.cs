using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
    private GameObject _dialoguePanel;
    private Text _nameText;
    private Text _dialogueText;
    private Button _continueButton;
    private int _dialogueIndex = 0;
    private string _npcName;
    private List<string> _dialogueLines;

    EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;

        _dialoguePanel = GameObject.Find("DialoguePanel");
        _nameText = _dialoguePanel.transform.Find("NamePanel").Find("Name").GetComponent<Text>();
        _dialogueText = _dialoguePanel.transform.Find("Dialogue").GetComponent<Text>();
        _continueButton = _dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        _continueButton.onClick.AddListener(ContinueDialogue);

        _dialoguePanel.SetActive(false);
    }

    public void AddDialogue(string name, string[] lines)
    {
        if (_eventSystem == null)
        {
            _eventSystem = EventSystem.current;
        }

        if (_eventSystem.currentSelectedGameObject != _continueButton.gameObject)
            _eventSystem.SetSelectedGameObject(_continueButton.gameObject);

        if (lines.Length == 0) return;

        _dialogueIndex = 0;
        _npcName = name;
        _dialogueLines = new List<string>(lines.Length);
        _dialogueLines.AddRange(lines);

        DisplayDialogue();
    }

    void DisplayDialogue()
    {
        _nameText.text = _npcName;
        _dialogueText.text = _dialogueLines[_dialogueIndex];
        _dialoguePanel.SetActive(true);
    }

    void ContinueDialogue()
    {
        if (_dialogueIndex < _dialogueLines.Count - 1)
        {

            _dialogueIndex++;
            _dialogueText.text = _dialogueLines[_dialogueIndex];
        }
        else
        {
            _dialoguePanel.SetActive(false);
        }
    }

}
