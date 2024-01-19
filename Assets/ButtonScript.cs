using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonBehavior : MonoBehaviour
{
    [SerializeField] private Image displayImage;

    private void Start()
    {
        // Ensure the display image is initially inactive
        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(false);
        }
    }

    public void SecretButton()
    {
        // Toggle the visibility of the display image
        if (displayImage != null)
        {
            displayImage.gameObject.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        GameObject mainMenuCanvas = GameObject.Find("MainMenuCanvas");
        mainMenuCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void NextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "LevelOne")
        {
            SceneManager.LoadScene("LevelTwo");
        }
        else if (scene.name == "LevelTwo")
        {
            SceneManager.LoadScene("LevelThree");
        }
        else if (scene.name == "LevelThree")
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}