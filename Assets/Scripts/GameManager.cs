using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public enum GameState
{
    Walking,
    Talking,
    Selling,
    Shopping,
    Creating,
    End
}



public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currState;
    public int money;
    public string currCustomer;

    private void Awake() { 
    
        if (Instance == null) 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject); 
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

    void Update()
    {
        
    }
}
