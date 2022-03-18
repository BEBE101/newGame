using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShakes : MonoBehaviour
{
    public float lerpTime;
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
        vel = new Vector3(Random.Range(-MoveValue.x, MoveValue.x), Random.Range(-MoveValue.y, MoveValue.y), Random.Range(-MoveValue.z, MoveValue.z));

        Vector3 vel2 = Vector3.Lerp(Vector3.zero, vel, Time.deltaTime * 4f);



        if (PlayerScipt.isCrouching)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(vel2), lerpTime * Time.deltaTime);
        }



    }
}
