using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class GameController : MonoBehaviourPunCallbacks
{
    
    
    public GameObject playerPrefab;
    private bool SpawnSwitch;
    private int[] playerScores;

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
        playerScores = new[] {3, 3};
        if(!SpawnSwitch)
        {
            SpawnPlayer();
            SpawnSwitch = !SpawnSwitch;
            Debug.Log("SpawnSwitch" + SpawnSwitch);
        }
        
    }
    private void SpawnPlayer()
    {
        Debug.Log("this is GameCOntroller's Spawn");
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        

        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0,0,0), Quaternion.identity);
    }
 
    private static GameController instance;
        public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
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

}   
