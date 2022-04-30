using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSway : MonoBehaviour
{

    public float LerpTime, Snapiness;

    [Space]
    public float RotationSnap;
    public float LerpRotate, MultiPlier;

    [Space]
    public float maxX;
    public float maxY;
    public float maxZ;
    [Space]
    public float maxRotateX;
    public float maxRotateY;





    Transform gunBarrel;
    Movement moveScript;
    Vector3 CurrentSway, SwaySnapiness, RotationSnappiness, RotationSway;
    Recoil camShakeScript;

    // Start is called before the first frame update
    void Start()
    {


        camShakeScript = FindObjectOfType<Recoil>();
        moveScript = FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

        float x = Mathf.Clamp(Input.GetAxis("Mouse X"), -maxX, maxX);
        float y = Mathf.Clamp(Input.GetAxis("Mouse Y"), -maxY, maxY);
        float z = Mathf.Clamp(Input.GetAxis("Vertical"), -maxZ, maxZ);
        float horizon = Mathf.Clamp(Input.GetAxis("Horizontal"), -maxZ, maxZ);

        Vector3 Move = -new Vector3(x, y, z) + -new Vector3(horizon, 0, 0f);

        float RotationY = Mathf.Clamp(Input.GetAxis("Mouse Y"), -maxRotateY, maxRotateY) * MultiPlier * 2.8f;
        float RotationX = Mathf.Clamp(Input.GetAxis("Mouse X"), -maxRotateX, maxRotateX / 1.5f) * MultiPlier * 2.3f;



        SwaySnapiness = Vector3.Lerp(SwaySnapiness, Move, Snapiness * Time.deltaTime);

        CurrentSway = Vector3.Lerp(SwaySnapiness, Vector3.zero, LerpTime * Time.deltaTime);


        RotationSnappiness = Vector3.Lerp(RotationSnappiness, new Vector3(RotationY, -RotationX), RotationSnap * Time.deltaTime);
        RotationSway = Vector3.Lerp(RotationSnappiness, Vector3.zero, LerpRotate * Time.deltaTime);


        transform.localPosition = Vector3.Lerp(transform.localPosition, CurrentSway, 50f * Time.deltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(RotationSway), 30f * Time.deltaTime);



    }



    public void recoil(float xRotation, float upRotation, float zMove)
    {


        float randomX = Random.Range(-xRotation, xRotation);
        RotationSnappiness += new Vector3(-upRotation, randomX);
        SwaySnapiness += new Vector3(-randomX / 10f, 0, -zMove);

        camShakeScript.rec(xRotation / 2f, upRotation / 2f);
    }


}
