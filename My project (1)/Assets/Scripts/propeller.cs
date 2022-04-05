using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propeller : MonoBehaviour
{





    float speed, rotation;


    // Update is called once per frame
    void Update()
    {

        speed = FindObjectOfType<EnemyDrone>()._propellerSpeed;


        rotation += speed * 2f;



        transform.localRotation = Quaternion.Euler(new Vector3(90, rotation));

    }
}
