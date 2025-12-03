using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
   public static Inventory Instance {  get; private set; }
   public List<Item> items = new List<Item>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void AddItem(Item newItem)
    {
        items.Add(newItem);
    }
    
    public void RemoveItem(Item ItemToRemove)
    {
        items.Remove(ItemToRemove);
    }

    public void PrintInventory()
    {
        foreach (Item item in items)
        {
            Debug.Log("Inventory Item: " + item.name);
        }
    }
}
