using System.Collections;
using System.Collections.Generic;
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

    void Start()
    {
        // Set up the buttons to call the appropriate functions when clicked
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    void StartGame()
    {
        // Load the game scene when the start button is clicked
        SceneManager.LoadScene("ThirdPersonMechanics");
    }

    void QuitGame()
    {
        // Quit the application when the quit button is clicked
        Application.Quit();
    }
}