using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{

    public float snappiness, lerpTime;

    Vector3 Current, Target;
    // Update is called once per frame
    void Update()
    {


        Current = Vector3.Lerp(Current, Vector3.zero, Time.deltaTime * snappiness);
        Target = Vector3.Slerp(Target, Current, lerpTime * Time.deltaTime);

        transform.localRotation = Quaternion.Euler(Target);

    }


    public void rec(float xRotation, float yRotation)
    {
        float randomX = Random.Range(-xRotation, xRotation);

        Current += new Vector3(-yRotation, randomX, 0f);
    }
}
