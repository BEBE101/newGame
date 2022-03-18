using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{

    CharacterController CC;
    Vector3 Velocity, Velocity2;
    public float Gravity, MoveSpeed, JumpHeight;
    [Range(0, 50f)]
    public float MovementSnapiness;


    [Header("Gravity")]

    public bool isGrounded;
    public LayerMask layer;
    public float RayDis;
    Vector3 hitnormal;
    public float slideFriction;

    [Header("Crouching")]
    public float crouchHeight;
    public bool isCrouching;

    float crouchSlide;



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
        SlopeSliding();
    }



    void _Gravity()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, RayDis, layer);


        if (isGrounded && Velocity2.y < 0) { Velocity2.y = Mathf.Lerp(Velocity2.y, -2f, 20f * Time.deltaTime); }
        //jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity2.y = Mathf.Sqrt(-2f * Gravity * JumpHeight);
        }
        //Gravity
        Velocity2.y += Gravity * Time.deltaTime;

        CC.Move(Velocity2 * Time.deltaTime);




    }

    void _Movement()
    {   //Storing Input
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");

        //Slide power calculation
        if (x != 0 || z != 0) crouchSlide = 1.3f;
        else crouchSlide = 0.6f;

        //running velocity smoothing
        Vector3 vel = Vector3.Lerp(Velocity, (x * transform.right + z * transform.forward).normalized, Time.deltaTime * MovementSnapiness);

        //velocity changing while crouching and running
        Velocity = isCrouching ? (transform.forward).normalized * crouchSlide : vel;



        //Adding movement;
        CC.Move(Velocity * MoveSpeed * Time.deltaTime);

        MoveSpeed = Mathf.Clamp(MoveSpeed, 0f, Mathf.Infinity);
    }
    void _crouching()
    {



        if (isGrounded && Input.GetKey(KeyCode.LeftControl))
        {

            isCrouching = true;
        }
        if (isCrouching)
        {
            MoveSpeed = Mathf.Lerp(MoveSpeed, 0f, Time.deltaTime * 1.2f);

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                MoveSpeed += 2.5f;
            }

        }
        else
        {
            MoveSpeed = 8f;

        }

        CC.Move((x * transform.right + transform.forward * z).normalized * Time.deltaTime);


        //Adding changes while crouching
        JumpHeight = isCrouching ? 1f : 2f; ;
        CC.height = isCrouching ? crouchHeight : 2f;
        RayDis = isCrouching ? 0.8f : 1.2f;
        slideFriction = isCrouching ? -500f : -150f;


        if (isCrouching && Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouching = false;

        }

    }


    void SlopeSliding()
    {
        //getting the slope angle

        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, RayDis, layer);
        hitnormal = hit.normal;
        float angle = Vector3.Angle(hit.normal, Vector3.up);

        //Checking if angle is smaller than the slopelimit

        if (isGrounded && angle < CC.slopeLimit)
        {
            float x = (1f - hitnormal.y) * hitnormal.x * (-1f - slideFriction * 2f);
            float z = (1f - hitnormal.y) * hitnormal.z * (-1f - slideFriction * 2f);

            //Adding force
            Vector3 Velocity1 = new Vector3(x, 0, z);
            CC.Move(Velocity1 * Time.deltaTime);

        }
    }





}
