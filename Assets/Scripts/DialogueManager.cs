using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;
    public Transform choicesContainer;
    public Button choiceButtonPrefab;

    private DialogueData currentDialogue;
    private DialogueNode currentNode;

    public void LoadDialogue(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Dialogues/" + fileName);
        if (jsonFile == null)
        {
            Debug.LogError($"Dialogue file '{fileName}' not found!");
            return;
        }

        currentDialogue = JsonUtility.FromJson<DialogueData>(jsonFile.text);
        characterNameText.text = currentDialogue.customerID;
        ShowNode("intro");
    }

    void ShowNode(string nodeId)
    {
        currentNode = currentDialogue.nodes.Find( n => n.id == nodeId );
        if (currentNode == null)
        {
            Debug.LogWarning($"Node {nodeId} is not found.");
            return;
        }

        dialogueText.text = currentNode.text;

        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);

        foreach (var choice in currentNode.choices)
        {
            Button newButton = Instantiate(choiceButtonPrefab, choicesContainer);
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
            newButton.onClick.AddListener(() => OnChoiceSelected(choice.nextId));
        }
    }

    void OnChoiceSelected(string nextId)
    {
        if (string.IsNullOrEmpty(nextId))
        {
            EndDialogue();
        }
        else
        {
            ShowNode(nextId);
        }
    }
    void EndDialogue()
    {
        dialogueText.text = "Conversation ended.";
        foreach (Transform child in choicesContainer ) 
            Destroy(child.gameObject);
    }

}
