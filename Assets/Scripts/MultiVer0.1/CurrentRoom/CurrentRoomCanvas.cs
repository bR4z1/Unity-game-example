using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomCanvas : MonoBehaviour {

    public void OnClickStartSync()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }
        PhotonNetwork.LoadLevel("GameMultiplayer"); // or 1 on LOAD SCENE NUMBER in build setting
    }
	
    public void OnClickStartDelayed()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            return;
        }

        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
        PhotonNetwork.LoadLevel("GameMultiplayer");
    }
}
