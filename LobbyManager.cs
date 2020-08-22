using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    
    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "Connecting To Master Server...";
    }
    
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected to Master Server";
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()} ";
    }
    
    public void Connect()
    {
        joinButton.interactable = false;

        if(PhotonNetwork.IsConnected)
        {
            connectionInfoText.text = "Connecting to Random Room...";
            PhotonNetwork.JoinRandomRoom();
        }
        else{
            connectionInfoText.text = "Offline : Connection Disabled - Try reconnect";
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "there is no empty room, creating new Room";
        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = 2});
    }
    
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room";
        //anybody in this room all together move to Game
        PhotonNetwork.LoadLevel("GameScene");
    }
}