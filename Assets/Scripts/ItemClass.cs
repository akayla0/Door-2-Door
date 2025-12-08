using System.Net.Http.Headers;
using UnityEngine;

[System.Serializable]
public class Item
{

    public string name;
    public int price;
    public string desc;
    public ItemType type;

    public Item(string name, int price)
        {
            this.name = name;
            this.price = price;
            this.desc = "";
    }

    }
    public enum ItemType
    {
        organic,
        metal,
        trash,
        magical,
        crafted,
        material
    } 