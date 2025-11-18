using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject controlScreen;

    void Awake()
    {
        startScreen = GameObject.Find("StartScreen");
        controlScreen = GameObject.Find("ControlsScreen");

        startScreen.SetActive(true);
        controlScreen.SetActive(false);
    }

    private void Update()
    {
        if (controlScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            startScreen.SetActive(true);
            controlScreen.SetActive(false);
        }
    }

    public void PlayGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void ControlScreen()
    {
        startScreen.SetActive(false);
        controlScreen.SetActive(true);

    }

}
