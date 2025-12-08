using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject inventoryPanel;
    public Transform itemsContainer;
    public GameObject itemButtonPrefab;

    [Header("Tabs")]
    public Button allTab;
    public Button ingredientsTab;
    public Button craftedTab;

    private void Start()
    {
        inventoryPanel.SetActive(false);

        if (allTab != null) allTab.onClick.AddListener(() => RefreshInventory("All"));
        if (ingredientsTab != null) ingredientsTab.onClick.AddListener(() => RefreshInventory("Ingredients"));
        if (craftedTab != null) craftedTab.onClick.AddListener(() => RefreshInventory("Crafted"));
    }


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);

            if (!isActive)
                RefreshInventory("All");
        }
    }

    public void RefreshInventory(string filter)
    {

        foreach (Transform child in itemsContainer)
            Destroy(child.gameObject); 

        List<Item> inv = GameManager.Instance.inventory;

        foreach (Item item in inv)
        {
            if (!PassesFilter(item, filter)) continue;

            GameObject buttonObj = Instantiate(itemButtonPrefab, itemsContainer);

            Button button = buttonObj.GetComponent<Button>();
            TextMeshProUGUI txt = buttonObj.GetComponentInChildren<TextMeshProUGUI>();

            txt.text = $"{item.name}\nPrice: {item.price}\n{item.desc}"; 

            button.onClick.AddListener(() => InspectItem(item));
        }

    }

    private bool PassesFilter(Item item, string filter)
    {
        if (filter == "All")
            return true;
        if (filter == "Ingredients") 
            return item.type == ItemType.material;
        if (filter == "Crafted")
            return item.type == ItemType.crafted;

        return true;
    }

    private void InspectItem(Item item)
    {
               Debug.Log("Item Opened " + item.name);
            // open a detail window at some point
    }
}
