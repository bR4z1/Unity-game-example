using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

    public static PlayerNetwork Instance;
    public string PlayerName { get; private set; }
    private PhotonView PhotonView;
    private int PlayersInGame = 0;

    private PlayerMovementMulti CurrentPlayer;

    public InputField NickNameField;

    private void Awake()
    {
        Instance = this;
        PlayerName = "Name#" + Random.Range(1, 99999);
        PhotonView = GetComponent<PhotonView>();

        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 50;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    private void Update()
    {
        //UpdateNickName  
        if (NickNameField.text.Length > 0)
        {
            PlayerName = NickNameField.text;
            PhotonNetwork.player.NickName = PlayerName;
        }
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameMultiplayer")
        {
            if (PhotonNetwork.isMasterClient)
            {
                MasterLoadedGame();
            }
            else
            {
                NonMasterLoadedGame();
            }
        }
    }

    private void MasterLoadedGame()
    {
        //PlayersInGame = 1;
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
        PhotonView.RPC("RPC_LoadGameOthers", PhotonTargets.Others);
    }

    private void NonMasterLoadedGame()
    {
        PhotonView.RPC("RPC_LoadedGameScene", PhotonTargets.MasterClient, PhotonNetwork.player);
    }

    [PunRPC]
    private void RPC_LoadGameOthers()
    {
        PhotonNetwork.LoadLevel("GameMultiplayer");
    }

    [PunRPC]
    private void RPC_LoadedGameScene(PhotonPlayer photonPlayer)
    {
        PlayerManagement.Instance.AddPlayerStats(photonPlayer);

        PlayersInGame++;
        if(PlayersInGame == PhotonNetwork.playerList.Length)
        {
            print("All players are in the game scene");
            PhotonView.RPC("RPC_CreatePlayer", PhotonTargets.All);
        }
    }

    public void NewHealth(PhotonPlayer photonPlayer, int health)
    {
        PhotonView.RPC("RPC_NewHealth", photonPlayer, health);
    }

    [PunRPC]
    private void RPC_NewHealth(int health)
    {
        if (CurrentPlayer == null)
        {
            return;
        }
        if (health <= 0)
        {
            PhotonNetwork.Destroy(CurrentPlayer.gameObject);
        }
        else
        {
            CurrentPlayer.Health = health;
        }
    }

    [PunRPC]
    private void RPC_CreatePlayer()
    {
        float randomValue = Random.Range(0f, 5f);
        GameObject obj = PhotonNetwork.Instantiate(Path.Combine("Prefabs", "NewPlayer"), Vector3.up * randomValue, Quaternion.identity, 0);
        CurrentPlayer = obj.GetComponent<PlayerMovementMulti>();
    }
}
