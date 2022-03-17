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
    Vector3 hitnormal;
    public float slideFriction;

    [Header("Crouching")]
    public float crouchHeight;
    public bool isCrouching;



    ///Input floats
    float x, z;


    void Start()
    {
        CC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _Movement();
        _Gravity();
        _crouching();

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
        //Gravity
        Velocity2.y += Gravity * Time.deltaTime;

        CC.Move(Velocity2 * Time.deltaTime);
        isGrounded = (Vector3.Angle(Vector3.up, hitnormal) <= CC.slopeLimit);



    }

    void _Movement()
    {   //Storing Input
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");



        Velocity = isCrouching ? transform.forward * 1.5f : x * transform.right + z * transform.forward;
        if (isGrounded)
        {
            float x = (1f - hitnormal.y) * hitnormal.x * (Gravity - slideFriction);
            float z = (1f - hitnormal.y) * hitnormal.z * (Gravity - slideFriction);

            Velocity += new Vector3(x, 0, z);


        }

        //Adding movement;
        CC.Move(Velocity * MoveSpeed * Time.deltaTime);

        MoveSpeed = Mathf.Clamp(MoveSpeed, 0f, Mathf.Infinity);
    }
    void _crouching()
    {



        if (isGrounded && Input.GetKeyDown(KeyCode.C))
        {


            isCrouching = true;
        }
        if (isCrouching)
        {
            MoveSpeed -= MoveSpeed / 1.4f * Time.deltaTime;

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                MoveSpeed += 2.5f;
            }

        }



        CC.height = isCrouching ? crouchHeight : 2f;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitnormal = hit.normal;
    }



}
