using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

    Transform trCannon;
    float zRotation = 5.0f;
    private float upOrDownSpeed = 30.0f;
    private bool isReady = false;
    public Transform CannonLOAD;
    public GameObject BallBullet;

    // create possition for Ball where he wait charge
    public Transform BallWaitCharge;

    // reload position in cannon. 
    Collider2D ReloadCollider;
    public GameObject ReloadObject;
    bool chargeCannon;

    // Create ball and active UI.slider charge
    GameObject instantiateBallCannon;
    private bool isActiveCharge;
    public GameObject LoadChargeBall;
    public Slider slider;

    bool DontCreateManyTimeBall = false;

    void Start () {
        trCannon = GetComponent<Transform>();
        ReloadCollider = ReloadObject.GetComponent<Collider2D>();
        //ReloadCollider = GameObject.FindGameObjectWithTag("CannonReload").GetComponent<Collider2D>();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player" && col.gameObject.transform.position.x < trCannon.position.x)
        {
            isReady = true;
            chargeCannon = ReloadObject.GetComponent<CannonFire>().ChargeCannon;
            //foreach (ContactPoint2D contact in col.contacts)
            //{
            //    Debug.DrawRay(contact.point, contact.normal * 10, Color.white);
            //}
        }  
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" 
            && col.gameObject.transform.position.x < trCannon.position.x)
        {
            isReady = false;
        }
    }

    void Update()
    {
        if (!DontCreateManyTimeBall && chargeCannon == true)
        {
            if (Input.GetKeyDown(KeyCode.F) && isReady)
            {
                // CREATE ball in another place and wait push UP.F
                instantiateBallCannon = Instantiate(BallBullet, BallWaitCharge.position, BallWaitCharge.rotation);
                isActiveCharge = true;
                LoadChargeBall.SetActive(true);
                DontCreateManyTimeBall = true;
            }
        }

        
        if (isActiveCharge)
        {
            instantiateBallCannon.GetComponent<CannonBall>().force += Time.deltaTime;
            slider.value = instantiateBallCannon.GetComponent<CannonBall>().force;
        }
        

        if (Input.GetKeyUp(KeyCode.F) && isReady)
        {
            isActiveCharge = false;
            LoadChargeBall.SetActive(false);
            Shoot();
            ReloadCollider.enabled = false;
            Invoke("WaitBall", 1f);
            
        }
    }

    void WaitBall()
    {
        ReloadCollider.enabled = true;
    }

    void Shoot()
    {
        if (chargeCannon)
        {
            instantiateBallCannon.GetComponent<Transform>().position = CannonLOAD.transform.position;
            DontCreateManyTimeBall = false;
            chargeCannon = false;
            ReloadObject.GetComponent<CannonFire>().ChargeCannon = false;
        }
    }


    void FixedUpdate ()
    {
        if (isReady)
        {
            //print(trCannon.rotation.z);
            if (Input.GetKey(KeyCode.B) && trCannon.transform.rotation.z < 0.50f)
            {
                zRotation += Time.deltaTime * upOrDownSpeed;
                transform.eulerAngles = new Vector3(0, 0, zRotation);
            }
            if (Input.GetKey(KeyCode.N) && trCannon.transform.rotation.z > -0.35f)
            {
                zRotation += Time.deltaTime * -upOrDownSpeed;
                transform.eulerAngles = new Vector3(0, 0, zRotation);
            }
        }
    }
}
