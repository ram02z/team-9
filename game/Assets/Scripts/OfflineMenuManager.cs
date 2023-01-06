using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OfflineMenuManager : MonoBehaviour
{
    public Button backButton;
    void Start()
    {
        backButton.onClick.AddListener(BackToMainMenu);
    }

    private void BackToMainMenu()
    {
        SceneManager.LoadScene("Scenes/Menu");
    }

}
