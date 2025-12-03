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

    [Header("Daily Shop Elements")]
    public int minDailyItems = 4;
    public int maxDailyItems = 10;

    [Header("Player Data")]
    public int playerMoney = 1000;

    private List<Item> shopItems = new List<Item>();

    void Start()
    {
        shopPanel.SetActive(true);
        LoadItemsFromJSON("Items");
        
        List<Item> dailyItems = GetRandomDailyItems();
        PopulateDailyShopUI(dailyItems);

        UpdateCurrencyUI();
    }

    void LoadItemsFromJSON(string fileName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(fileName);
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
        if (playerMoney >= item.price)
        {
            playerMoney -= item.price;
            UpdateCurrencyUI();
            Debug.Log($"Purchased: {item.name}");

            //add item to inventory at some point lol
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }
    void UpdateCurrencyUI()
    {
        moneyText.text = $"Money: ${playerMoney}";
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
}



