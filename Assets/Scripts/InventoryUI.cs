using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;


    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Tab))
        {
            bool isActive = inventoryPanel.activeSelf;
            inventoryPanel.SetActive(!isActive);
        }
    }
}
