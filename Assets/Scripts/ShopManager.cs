using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject shopPanel;
    public Transform itemsContainer;
    public GameObject itemButtonPrefab;
    public TextMeshProUGUI moneyText;

    [Header("Player Data")]
    public int playerMoney = 1000;

    private List<Item> shopItems = new List<Item>();

    void Start()
    {
        shopPanel.SetActive(true);
        LoadItemsFromJSON("Items");
        PopulateShopUI();
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

        shopItems = JsonUtilityWrapper.FromJsonArray<Item>(jsonFile.text);
    }

    void PopulateShopUI()
    {
        foreach (Transform child in itemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in shopItems)
        {
            GameObject newButtonObj = Instantiate(itemButtonPrefab, itemsContainer);
            Button newButton = newButtonObj.GetComponent<Button>();

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = $"{item.name}\nPrice: ${item.price}";

            newButton.onClick.AddListener(() => BuyItem(item));
        }
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

    }

