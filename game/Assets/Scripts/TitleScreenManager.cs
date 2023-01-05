using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager instance;
    [SerializeField] private RoomManager roomManager;


    [Header("UI Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject PlayerNamePanel;
    [SerializeField] private GameObject HostOrJoinPanel;
    [SerializeField] private GameObject EnterIPAddressPanel;


    [Header("Enter IP UI")]
    [SerializeField] private TMP_InputField IpAddressField;

    [Header("Misc. UI")]
    [SerializeField] private Button returnToMainMenu;

    void Awake()
    {
        MakeInstance();
        ReturnToMainMenu();
    }

    void MakeInstance()
    {
        if (instance == null)
            instance = this;
    }

    public void ReturnToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        // PlayerNamePanel.SetActive(false);
        HostOrJoinPanel.SetActive(false);
        // EnterIPAddressPanel.SetActive(false);
        // returnToMainMenu.gameObject.SetActive(false);
    }

    public void StartRoom()
    {
        //SceneManager.LoadScene("Gameplay");
        mainMenuPanel.SetActive(false);
        // PlayerNamePanel.SetActive(true);
        // GetSavedPlayerName();
        // returnToMainMenu.gameObject.SetActive(true);
    }

    public void StartGame() {
        mainMenuPanel.SetActive(false);
        HostOrJoinPanel.SetActive(true);
    }

    public void HostGame()
    {
        Debug.Log("Hosting a game...");
        roomManager.StartHost();
        // HostOrJoinPanel.SetActive(false);
        // returnToMainMenu.gameObject.SetActive(false);
    }

    public void JoinGame()
    {
        // HostOrJoinPanel.SetActive(false);
        // EnterIPAddressPanel.SetActive(true);
        roomManager.networkAddress = "localhost";
        roomManager.StartClient();
    }
}
