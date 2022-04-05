using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public Transform Player;
    public Camera cam;
    public float InitialFoV;


    [Range(30, 3000)]
    public float Sensitivity;
    public Vector3 Offset;
    float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        InitialFoV = cam.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InitialFoV, Time.deltaTime * 2f);


        float x = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime;
        float y = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime;



        xRotation -= y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0).normalized;
        Player.Rotate(Vector3.up.normalized * x);



        transform.position = Player.position + Offset;
    }
}
