using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI characterNameText;
    public Transform choicesContainer;
    public Button choiceButtonPrefab;
    public static DialogueManager Instance;

    private DialogueData currentDialogue;
    private DialogueNode currentNode;
    private DialogueManager dialogueManager;
    

    void Start()
    {
        dialogueManager = Object.FindFirstObjectByType<DialogueManager>();
        DialoguePanel.SetActive(false);

        string customer = GameManager.Instance.currCustomer;
        LoadDialogue(customer);
    }

    private void Awake()
    {
        Instance = this;
    }



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

        DialoguePanel.SetActive (true);

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
            if (nextId.StartsWith("bad_end") || nextId.StartsWith("end"))
            {
                ShowNode(nextId);
                StartCoroutine(ExitAfterDelay(2.5f));
            }
            if (nextId.StartsWith("items"))
            {
                string shop = "ShopScene";
                ShowNode(nextId);

                GameManager.Instance.SetState(GameState.Shopping);
                StartCoroutine(LoadSceneDelayed(shop, 2f));
            }
            else
            {
                ShowNode(nextId);
            }
                
        }
    }

    IEnumerator ExitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndDialogue();
        ExitDialogueScene();
    }
    IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }

    void EndDialogue()
    {
        dialogueText.text = "Conversation ended.";
        foreach (Transform child in choicesContainer ) 
            Destroy(child.gameObject);
    }

    void ExitDialogueScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
}
