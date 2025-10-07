using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net.Http.Headers;

public class TextManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI customerNameText;
    public TextMeshProUGUI dialogueText;
    public Button optionOne;
    public Button optionTwo;

    private string currentCustomer;

    void Start()
    {
        currentCustomer = GameManager.Instance.currCustomer;
        customerNameText.text = currentCustomer;

        StartDialogue();
    }

    void StartDialogue()
    {
        dialogueText.text = $"{currentCustomer} opens the door";

        optionOne.GetComponentInChildren<TextMeshProUGUI>().text = "Try to sell";
        optionTwo.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";

        optionOne.onClick.RemoveAllListeners();
        optionTwo.onClick.RemoveAllListeners();

        optionOne.onClick.AddListener(() => HandleChoice(true));
        optionTwo.onClick.AddListener(() => HandleChoice(false));

    }

    void HandleChoice(bool sold)
    {
        if (sold)
        {
            dialogueText.text = "Item sold! You earn {itemValue}";
            //GameManager.Instance.money += itemValue;
        }
        else
        {
            dialogueText.text = "Failed to sell!";
        }

        optionOne.interactable = false;
        optionTwo.interactable = false;

        Invoke(nameof(ReturnToMain), 4f);
    }

    void ReturnToMain()
    {
        SceneManager.LoadScene("MainScene");
    }
}
