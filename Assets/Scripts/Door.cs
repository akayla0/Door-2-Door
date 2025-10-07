using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string customerName;
    public string dialogueSceneName = "ChatView";

    private bool playerInRange = false;
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.currCustomer = customerName;
            GameManager.Instance.SetState(GameState.Talking);
            SceneManager.LoadScene(dialogueSceneName);
            //input: dialouge interaction
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
            playerInRange = false;
    }
}
