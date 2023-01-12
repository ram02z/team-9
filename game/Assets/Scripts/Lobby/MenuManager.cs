using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // The main menu buttons
    public Button startButton;
    public Button quitButton;

    // The game object that contains the menu screen
    public GameObject menuScreen;

    /// <summary>
    /// Setup the event listeners for buttons
    /// </summary>
    void Start()
    {
        // Set up the buttons to call the appropriate functions when clicked
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    /// <summary>
    /// Load the game scene
    /// </summary>
    void StartGame()
    {
        // Load the game scene when the start button is clicked
        SceneManager.LoadScene("Scenes/Offline");
    }

    /// <summary>
    /// Quit the application
    /// </summary>
    void QuitGame()
    {
        // Quit the application when the quit button is clicked
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #endif
    }
}
