using System.Net.Http.Headers;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public Sprite itemIcon;
    public float itemValue;

    public Item(string name, int id, Sprite icon, float value)
    {
        itemName = name;
        itemID = id;
        itemIcon = icon;
        itemValue = value;
    }

}
