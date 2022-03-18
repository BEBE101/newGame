using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakes : MonoBehaviour
{
    public float lerpTime, recoil;
    public Vector3 MoveValue;
    Vector3 vel;
    Movement PlayerScipt;


    private void Awake()
    {
        PlayerScipt = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        vel = new Vector3(recoil, Random.Range(-MoveValue.y, MoveValue.y), Random.Range(-MoveValue.z, MoveValue.z));

        Vector3 vel2 = Vector3.Lerp(vel, Vector3.zero, Time.deltaTime * 4f);



        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(vel2), lerpTime * Time.deltaTime);
        if (Input.GetButtonDown("Fire1"))
        {
            vel.x += recoil;
        }


    }
}
