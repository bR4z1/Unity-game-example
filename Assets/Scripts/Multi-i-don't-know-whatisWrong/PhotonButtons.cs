using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PhotonButtons : MonoBehaviour {

    public photonHandler pHandler;
    public InputField createRoomInput, JoinRoomInput;

    public void onClickCreateRoom()
    {
        pHandler.createNewRoom();
    }

    public void onClickJoinRoom()
    {
        pHandler.joinOrCreateRoom();
    }

    //private void OnJoinedRoom()
    //{
    //    pHandler.moveScene(); 
    //    Debug.Log("We are connected to the room!");
    //}

}
