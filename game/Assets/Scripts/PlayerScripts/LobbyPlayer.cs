using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System.Linq;

public class LobbyPlayer : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandlePlayerNameUpdate))] public string PlayerName;
    [SyncVar] public int ConnectionId;

    [Header("Game Info")]
    public bool IsGameLeader = false;
    [SyncVar(hook = nameof(HandlePlayerReadyStatusUpdate))] public bool IsReady = false;
    [Header("UI")]
    [SerializeField] private GameObject PlayerLobbyUI;
    [SerializeField] private GameObject Player1ReadyPanel;
    [SerializeField] private GameObject Player2ReadyPanel;
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private Button readyButton; 

    private const string PlayerPrefsNameKey = "PlayerName";

    private NetworkManagerCC game;
    private NetworkManagerCC Game
    {
        get
        {
            if (game != null)
            {
                return game;
            }
            return game = NetworkManagerCC.singleton as NetworkManagerCC;
        }
    }

    public override void OnStartClient()
    {
        Game.LobbyPlayers.Add(this);
        Debug.Log("Added to GamePlayer list: " + this.PlayerName);
    }

    public override void OnStopClient()
    {
        Debug.Log(PlayerName + " is quiting the game.");
        Game.LobbyPlayers.Remove(this);
        Debug.Log("Removed player from the GamePlayer list: " + this.PlayerName);
    }

    public override void OnStartAuthority()
    {
        CmdSetPlayerName(PlayerPrefs.GetString(PlayerPrefsNameKey));
        if (!PlayerLobbyUI.activeInHierarchy)
	        PlayerLobbyUI.SetActive(true);

        Debug.Log("OnStartAuthority: " + this.PlayerName);
        gameObject.name = "LocalLobbyPlayer";
        Debug.Log("OnStartAuthority renamed: " + this.PlayerName);
    }

    [Command]
    private void CmdSetPlayerName(string playerName)
    {
        PlayerName = playerName;
        Debug.Log("Player display name set to: " + playerName);
    }


    // COMMENTED OUT TO FIND OUT ISSUES IN EXISTING CODE.


    public void HandlePlayerNameUpdate(string oldValue, string newValue)
    {
        Debug.Log("Player name has been updated for: " + oldValue + " to new value: " + newValue);
        UpdateLobbyUI();
    }
    public void HandlePlayerReadyStatusUpdate(bool oldValue, bool newValue)
    {
        Debug.Log("Player ready status has been has been updated for " + this.PlayerName + ": " + oldValue + " to new value: " + newValue);
        UpdateLobbyUI();
    }

    public void UpdateLobbyUI()
    {
        Debug.Log("Updating UI for: " + this.PlayerName);
        GameObject localPlayer = GameObject.Find("LocalLobbyPlayer");
        if (localPlayer != null)
        {
            // localPlayer.GetComponent<LobbyPlayer>().ActivateLobbyUI();
        }
    }

    // public void ActivateLobbyUI()
    // {
    //     Debug.Log("Activating lobby UI");
    //     if (!PlayerLobbyUI.activeInHierarchy)
    //         PlayerLobbyUI.SetActive(true);
    //     if (Game.LobbyPlayers.Count() > 0)
    //     {
    //         Player1ReadyPanel.SetActive(true);
    //         Debug.Log("Player1 Ready Panel activated");
    //         Player2ReadyPanel.SetActive(false);
    //     }
    //     else
    //     {
    //         Debug.Log("Player1 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
    //     }
    //     if (Game.LobbyPlayers.Count() > 1)
    //     {
    //         Player2ReadyPanel.SetActive(true);
    //         Debug.Log("Player2 Ready Panel activated");
    //     }
    //     else
    //     {
    //         Debug.Log("Player2 Ready Panel not activated. Player count: " + Game.LobbyPlayers.Count().ToString());
    //     }
    //     UpdatePlayerReadyText();
    // }

    // public void UpdatePlayerReadyText()
    // {
    //     if (Player1ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 0)
    //     {
    //         foreach (Transform childText in Player1ReadyPanel.transform)
    //         {
    //             if (childText.name == "Player1Name")
    //                 childText.GetComponent<Text>().text = Game.LobbyPlayers[0].PlayerName;
    //             if (childText.name == "Player1ReadyText")
    //             {
    //                 bool isPlayerReady = Game.LobbyPlayers[0].IsReady;
    //                 if (isPlayerReady)
    //                 {
    //                     childText.GetComponent<Text>().text = "Ready";
    //                     childText.GetComponent<Text>().color = Color.green;
    //                 }
    //                 else
    //                 {
    //                     childText.GetComponent<Text>().text = "Not Ready";
    //                     childText.GetComponent<Text>().color = Color.red;
    //                 }
    //             }
    //         }
    //     }
    //     if (Player2ReadyPanel.activeInHierarchy && Game.LobbyPlayers.Count() > 1)
    //     {
    //         foreach (Transform childText in Player2ReadyPanel.transform)
    //         {
    //             if (childText.name == "Player2Name")
    //                 childText.GetComponent<Text>().text = Game.LobbyPlayers[1].PlayerName;
    //             if (childText.name == "Player2ReadyText")
    //             {
    //                 bool isPlayerReady = Game.LobbyPlayers[1].IsReady;
    //                 if (isPlayerReady)
    //                 {
    //                     childText.GetComponent<Text>().text = "Ready";
    //                     childText.GetComponent<Text>().color = Color.green;
    //                 }
    //                 else
    //                 {
    //                     childText.GetComponent<Text>().text = "Not Ready";
    //                     childText.GetComponent<Text>().color = Color.red;
    //                 }
    //             }
    //             Debug.Log("Updated Player2 Ready panel with player name: " + Game.LobbyPlayers[1].PlayerName + " and ready status: " + Game.LobbyPlayers[1].IsReady);
    //         }
    //     }
    // }

    // [Command]
    // public void CmdReadyUp()
    // {
    //     IsReady = !IsReady;
    //     Debug.Log("Ready status changed for: " + PlayerName);
    // }

}
