using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementMulti : Photon.MonoBehaviour {

    private PhotonView PhotonView;
    private Vector3 TargetPosition;
    private Quaternion TargetRotation;
    public float Health;

    GameObject textGO;
    Text speed;

    // Camera
    public GameObject playerCamera;
    GameObject mCam;

    // NickName
    public TextMesh PlayerName;
    float posYnick;
    float posYplayer;

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
        textGO = GameObject.FindGameObjectWithTag("TextSpeed");
        speed = textGO.GetComponent<Text>();
        speed.text = "";
        if (PhotonView.isMine)
        {
            mCam = GameObject.FindGameObjectWithTag("MainCamera");
            mCam.SetActive(false);
            playerCamera.SetActive(true);
        }
        posYnick = PlayerName.GetComponent<Transform>().position.y;
        posYplayer = gameObject.GetComponent<Transform>().position.y;
        posYnick = posYplayer + 2.3f;
        PlayerName.text = PhotonView.owner.NickName;
    }

    // Update is called once per frame
    void Update () {
        if (PhotonView.isMine)
        {
            CheckInput();
        }
        else
        {
            SmoothMove();
        }
	}

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            //stream.SendNext(Health);
        }
        else
        {
            TargetPosition = (Vector3)stream.ReceiveNext();
            TargetRotation = (Quaternion)stream.ReceiveNext();
            //Health = (int)stream.ReceiveNext();
        }
    }

    private void SmoothMove()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.3f);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, TargetRotation, 500 * Time.deltaTime);
    }

    private void CheckInput()
    {
        float moveSpeed = 30f; // ???

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontal, vertical);
        movement.Normalize();
        gameObject.GetComponent<Rigidbody2D>().velocity = movement * moveSpeed;

        //var move = new Vector3(vertical, 0);
        speed.text = gameObject.GetComponent<Rigidbody2D>().velocity.ToString()+"         HEALTH "+Health.ToString();
        //transform.position += move * moveSpeed * Time.deltaTime;
        //transform.Translate(new Vector3(horizontal, vertical) * moveSpeed);
    }
}
