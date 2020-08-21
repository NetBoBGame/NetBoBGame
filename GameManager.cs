using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Text scoreText;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    private int[] playerScores;

    private void Start()
    {
        playerScores = new[] {0,0};
        //each Player activation each 1 time.
        SpawnPlayer();
        if(PhotonNetwork.IsMasterClient)
        {
            SpawnBall();
        }
    }

    private void SpawnPlayer()
    {
        //inside we handle character 0, 1
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber -1 ;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

        PhotonNetwork.Instantiate(playerPrefab.name,spawnPosition.position,spawnPosition.rotation);
    }

    private void SpawnBall()
    {
        PhotonNetwork.Instantiate(ballPrefab.name,Vector2.zero, Quaternion.identity);
    }

    public override void OnLeftRoom()
    {
        //one person is left except others.
        SceneManager.LoadScene("Lobby");
    }
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void AddScore(int playerNumber, int score)
    {
        //only master update score.
        if(!PhotonNetwork.IsMasterClient)
        {
            return;
        }
        playerScores[playerNumber -1 ] += score;

        //everybody shouting.
        photonView.RPC("RPCUpdateScoreText", RpcTarget.All,playerScores[0].ToString(),playerScores[1].ToString());
    }


    
    [PunRPC]
    private void RPCUpdateScoreText(string player1ScoreText, string player2ScoreText)
    {
        scoreText.text = $"{player1ScoreText} : {player2ScoreText}";
    }
}