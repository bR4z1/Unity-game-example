using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour {

    public Transform firePoint;
    public GameObject CherryBullet;
    public GameObject canvas;


    // Update is called once per frame
    void Update () {


        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (PickUpCherry.value_shoot > 0)
        {
            Instantiate(CherryBullet, firePoint.position, firePoint.rotation);
        }
        else
        {
            canvas.SetActive(true);
            StartCoroutine(delayTextandFade());
            
        }
    }


    IEnumerator delayTextandFade()
    {
        yield return new WaitForSeconds(1);
        canvas.SetActive(false);
    }
}
