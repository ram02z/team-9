using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("Player Components")]
    public Image image;

    [Header("Child Text Objects")]
    public Text playerNameText;
    public Text playerDataText;

    public Button readyUpButton;
    [SerializeField] private TextMeshProUGUI ReadyBtnText;

    // Sets a highlight color for the local player
    public void SetLocalPlayer()
    {
        // add a visual background for the local player in the UI
        image.color = new Color(1f, 1f, 1f, 0.1f);
    }

    public Button getReadyBtn() {
        return readyUpButton;
    }

    public TextMeshProUGUI getReadyBtnText() {
        return ReadyBtnText;
    }

    // This value can change as clients leave and join
    public void OnPlayerNumberChanged(byte newPlayerNumber)
    {
        playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
    }

    // Random color set by Player::OnStartServer
    public void OnPlayerColorChanged(Color32 newPlayerColor)
    {
        playerNameText.color = newPlayerColor;
    }

    // This updates from Player::UpdateData via InvokeRepeating on server
    public void OnPlayerDataChanged(ushort newPlayerData)
    {
        // Show the data in the UI
        playerDataText.text = string.Format("Data: {0:000}", newPlayerData);
    }

    public void onReadyChange(bool isReady) {
        if(isReady) { ReadyBtnText.text = "Cancel"; }
        else { ReadyBtnText.text = "Ready Up"; }
    }
}