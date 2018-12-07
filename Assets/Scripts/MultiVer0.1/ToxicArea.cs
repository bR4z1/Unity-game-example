using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicArea : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D col)
    {
        //if (!PhotonNetwork.isMasterClient)
        //{
        //    return;
        //}
        PhotonView photonView = col.GetComponent<PhotonView>();
        if (col.gameObject.tag == "PlayerMultiGame")
        {
            PlayerManagement.Instance.ModifyHealth(photonView.owner, -10);
        }
        //PhotonView photonView = col.GetComponent<PhotonView>();
        //if(photonView != null && photonView.isMine)
        //{
        //    PlayerManagement.Instance.ModifyHealth(photonView.owner, -10);
        //}
    }
}
