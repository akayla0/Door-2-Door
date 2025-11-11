using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [Header("Dialogue Settings")]
    public string dialogueSceneName = "ChatView";

    [Header("Customer Settings")]
    public string customerName;
    public string[]possibleCustomers = {"normal_customer", "angry_customer", "eccentric_customer", "goth_customer", "the_wizard"};

    private bool playerInRange = false;

    void Start()
    {
        if (string.IsNullOrEmpty(customerName))
        {
            customerName = possibleCustomers[Random.Range(0,possibleCustomers.Length)];
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.currCustomer = customerName;

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                GameManager.Instance.playerData = new PlayerData
                {
                    position = player.transform.position,
                };
            }
            GameManager.Instance.SetState(GameState.Talking);
            SceneManager.LoadScene(dialogueSceneName);
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
