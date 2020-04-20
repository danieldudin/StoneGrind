using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem Instance { get; set; }
    public GameObject dialogueCanvas;
    public GameObject dialoguePanel;
    public string npcName;
    public List<string> dialogueLines = new List<string>();

    Button continueButton;

    TextMeshProUGUI dialogueText, nameText;
    int dialogueIndex;

    // Start is called before the first frame update
    void Awake()
    {

        continueButton = dialoguePanel.transform.Find("Continue").GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });

        dialogueText = dialoguePanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        nameText = dialoguePanel.transform.Find("Name").GetChild(0).GetComponent<TextMeshProUGUI>();

        dialogueCanvas.SetActive(false);
        dialoguePanel.SetActive(false);

        if (Instance != null && Instance != this) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
        }
    }

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);

        this.npcName = npcName;

        CreateDialogue();
    }

    public void CreateDialogue() 
    {
        dialogueText.text = dialogueLines[dialogueIndex];
        nameText.text = npcName;
        dialogueCanvas.SetActive(true);
        dialoguePanel.SetActive(true);
    }
    public void ContinueDialogue() 
    {
        if (dialogueIndex < dialogueLines.Count - 1)
        {
            dialogueIndex++;
            dialogueText.text = dialogueLines[dialogueIndex];
        }
        else {
            dialogueCanvas.SetActive(false);
            dialoguePanel.SetActive(false);
        }
    }
}
