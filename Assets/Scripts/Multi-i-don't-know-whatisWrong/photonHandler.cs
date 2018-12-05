using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class photonHandler : MonoBehaviour {

    public PhotonButtons photonB;
    public GameObject mainPlayer;

    private void Awake()
    {
        DontDestroyOnLoad(this.transform);

        PhotonNetwork.sendRate = 30;
        PhotonNetwork.sendRateOnSerialize = 20;

        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public void createNewRoom()
    {
        PhotonNetwork.CreateRoom(photonB.createRoomInput.text, new RoomOptions() { MaxPlayers = 3 }, null);
    }

    public void joinOrCreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 3;
        PhotonNetwork.JoinOrCreateRoom(photonB.JoinRoomInput.text, roomOptions, TypedLobby.Default);
    }

	public void moveScene()
    {
        photonB = null;
        PhotonNetwork.LoadLevel("MultiplayerSceneGame");
    }

    private void OnJoinedRoom()
    {
        moveScene();
        Debug.Log("We are connected to the room!");
    }

    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "MultiplayerSceneGame")
        {
            spawnPlayer();
        }
    }

    private void spawnPlayer()
    {
        PhotonNetwork.Instantiate(mainPlayer.name, mainPlayer.transform.position, mainPlayer.transform.rotation, 0);
    }
}
