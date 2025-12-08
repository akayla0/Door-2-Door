using UnityEngine;
using System.Collections.Generic;

public enum GameState
{
    Walking,
    Talking,
    Selling,
    Shopping,
    Creating,
    End
}

[System.Serializable]
    public class HouseData
    {
        public Vector3 position;
        public string customerType;

        public Color wallColor;
        public Color roofColor;
        public Color curtainColor;

        public bool visited;
        //add little lights that turn off after visiting :)
    }
[System.Serializable]
public class BarrierData
{
    public Vector3 position;
    public Vector3 scale;
}
[System.Serializable]
public class PlayerData
{
    public int money;
    public Vector3 position;
}


public class GameManager : MonoBehaviour
{
     public static GameManager Instance;
     public GameState currState;
     public int money;
     public string currCustomer;


    public List<HouseData> houseList = new List<HouseData>();
    public BarrierData barrierData;
    public BarrierData leftBarrierData;
    public BarrierData rightBarrierData;
    public PlayerData playerData;

    public List<Item> inventory = new List<Item>();

    private void Awake() { 
    
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (money == 0)
                money = 50;
        }
        else Destroy(gameObject); 
    }

    public void SaveHouseList(List<HouseData> newHouseList)
    {
        houseList = newHouseList;
    }

    public void ClearHouseList()
    {
        houseList.Clear();
    }
    void Start()
    {
        SetState(GameState.Walking);
    }

    public void SetState(GameState newState)
    {
        currState = newState;

        switch (newState)
        {
            case GameState.Walking:

                break;
            case GameState.Talking:

                break;
            case GameState.Selling: 
                
                break;
            case GameState.Shopping:

                break;
            case GameState.Creating:

                break;
            case GameState.End:

                break;
        }
    }

    public void AddItem (Item item)
    {
        inventory.Add(item);
        Debug.Log("Added " + item.name + " to inventory.");
    }

    public bool SpendMoney(int amount)
    {
        if (money >= amount)
        {
            money -= amount;
            return true;
        }
        else
        {
            Debug.Log("Not enough money!");
            return false;
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }
}
