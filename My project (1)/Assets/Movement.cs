using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    CharacterController CC;
    Vector3 Velocity, Velocity2;
    public float Gravity, MoveSpeed, Jumpspeed;


    [Header("Gravity")]

    public bool isGrounded;
    public LayerMask layer;
    public float RayDis;



    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _Movement();
        _Gravity();


    }


    void _Gravity()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, RayDis, layer);


        if (isGrounded && Velocity2.y < 0) { Velocity2.y = Mathf.Lerp(Velocity2.y, -2f, 20f * Time.deltaTime); }
        //jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity2.y = Mathf.Sqrt(-2f * Gravity * Jumpspeed);

        }

        Velocity2.y += Gravity * Time.deltaTime;

        CC.Move(Velocity2 * Time.deltaTime);



    }

    void _Movement()
    {   //Storing Input
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Velocity = x * transform.right + z * transform.forward;

        //Adding movement;
        CC.Move(Velocity * MoveSpeed * Time.deltaTime);


    }
}
