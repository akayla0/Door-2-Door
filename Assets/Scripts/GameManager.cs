using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;
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
        public Color houseColor;
        public bool visited;
        //add little lights that turn off after visiting :)
    }


public class GameManager : MonoBehaviour
{
     public static GameManager Instance;
     public GameState currState;
     public int money;
     public string currCustomer;


    public List<HouseData> houseList = new List<HouseData>();

    private void Awake() { 
    
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
}
