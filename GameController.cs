using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameController : MonoBehaviourPunCallbacks
{
    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    private bool SpawnSwitch;
    public int[] playerScores;
    public static GameController Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameController>();
            return instance;
        }
    }
   
    private void Start()
    {
        playerScores = new[] {0, 0};
        if(!SpawnSwitch)
        {
            SpawnPlayer();
            SpawnSwitch = !SpawnSwitch;
            Debug.Log("SpawnSwitch" + SpawnSwitch);
        }
        
    }
  
    private static GameController instance;
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Damaged(int playerNumber, int score)
    {
        //need to fill the other attack me . 
        playerScores[playerNumber - 1] -= score;
        if(playerScores[playerNumber -1] <= 0)
        {
            OnLeftRoom();
        }
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}   
