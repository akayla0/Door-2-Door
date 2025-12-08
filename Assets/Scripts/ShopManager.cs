using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using UnityEngine.Pool;


public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject shopPanel;
    public Transform itemsContainer;
    public GameObject itemButtonPrefab;
    public TextMeshProUGUI moneyText;
    public Button openButton;
    public Button closeButton;

    [Header("Daily Shop Elements")]
    public int minDailyItems = 4;
    public int maxDailyItems = 10;

    [Header("Player Data")]
    public int playerMoney = 1000;

    private List<Item> shopItems = new List<Item>();

    void Start()
    {
        shopPanel.SetActive(false);
        LoadItemsFromJSON("Items");
        
        List<Item> dailyItems = GetRandomDailyItems();
        PopulateDailyShopUI(dailyItems);

        UpdateCurrencyUI();
    }

    void LoadItemsFromJSON(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("Items/" + fileName);
        if (jsonFile == null)
        {
            Debug.LogError($"Items file '{fileName}' not found!");
            return;
        }
        ItemCollection loaded = JsonUtility.FromJson<ItemCollection>(jsonFile.text);
        shopItems = new List<Item>(loaded.items);
    }

    void BuyItem(Item item)
    {
        if (GameManager.Instance.SpendMoney(item.price))
        {
            GameManager.Instance.AddItem(item);
            UpdateCurrencyUI();
            Debug.Log($"Purchased: {item.name}");
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
    void UpdateCurrencyUI()
    {
        moneyText.text = $"Money: ${GameManager.Instance.money}";
    }

    List<Item> GetRandomDailyItems()
    {
        List<Item> dailyItems = new List<Item>(shopItems);

        for (int i = 0; i <  dailyItems.Count; i++)
        {
            Item temp = dailyItems[i];
            int randomIndex = Random.Range(i, dailyItems.Count);
            dailyItems[i] = dailyItems[randomIndex];
            dailyItems[randomIndex] = temp;
        }

        int amountToTake = Random.Range(minDailyItems,maxDailyItems + 1);

        amountToTake = Mathf.Min(amountToTake, dailyItems.Count);

        return dailyItems.GetRange(0, amountToTake);

    }

    void PopulateDailyShopUI(List<Item> dailyItems)
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }
        
        foreach (var item in dailyItems)
        {
            GameObject buttonObj = Instantiate(itemButtonPrefab, itemsContainer);
            Button newButton = buttonObj.GetComponent<Button>();
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();

            buttonText.text = $"{item.name}\nPrice: ${item.price}";

            newButton.onClick.AddListener(() => BuyItem(item));
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
        openButton.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }

    public void ExitScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }
        
}



