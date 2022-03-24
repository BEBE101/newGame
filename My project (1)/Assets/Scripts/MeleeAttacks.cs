using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    //PowerSlice
    public Transform SlicePoint, Cam;
    public float SliceSpeed, SliceDis, SliceMaxTime, SliceDamage;
    public bool isSlicing;


    [Header("GroundSlam")]
    [Space]
    public float MinDamage;
    public float MaxDamage;

    [Range(0, 200f)]
    public float SlamSpeed, SlamSpeedLerpTime, SlamStopTime;
    public bool isSlamingGround;
    float GroundDis;






    Movement Player;
    float time, damage, dis;




    private void Start()
    {
        Player = FindObjectOfType<Movement>();
    }
    private void Update()
    {
        if (Player.isGrounded) Slice();
        else GroundSlam();

        if (isSlicing) SliceMove(time);
        else return;
    }


    void Slice()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            SlicePoint.localPosition = new Vector3(0, 0, SliceDis);
            isSlicing = true;
            time = Time.time + SliceMaxTime;

        }




    }

    void SliceMove(float t)
    {
        //Distance
        float dis = Vector3.Distance(SlicePoint.position, transform.position);
        float Sped = Mathf.Clamp(dis, 10f, 50f);


        //Motion
        Vector3 move = (SlicePoint.position - transform.position).normalized;
        Player.CC.Move(move * Sped * SliceSpeed * Time.deltaTime);


        //Stopping
        if (t < Time.time)
        {
            isSlicing = false;
            LastMove();

        }
    }


    void LastMove()
    {
        if (!isSlicing)
        {
            float sped = 5f;
            sped = sped - 3f * Time.deltaTime;
            sped = Mathf.Clamp(sped, 0, Mathf.Infinity);
            Player.Velocity = transform.forward * sped;

            if (sped < 0.5f) CancelInvoke("LastMove");

        }
    }



    void GroundSlam()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isSlamingGround = true;

            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
            GroundDis = hit.distance;
        }


        if (isSlamingGround) SlamMove();

    }
    void SlamMove()
    {
        float speed = Mathf.Lerp(0, SlamSpeed, GroundDis * SlamSpeedLerpTime * Time.deltaTime);
        Player.CC.Move(Vector3.down.normalized * 10f * speed * Time.deltaTime);
        Player.CanMove = false;


        //DistanceCheck
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
        float dis = hit.distance;


        if (dis < 1.4f) StopMotion();
        Invoke("StopMotion", SlamStopTime);


    }

    void StopMotion()
    {
        isSlamingGround = false;
        isSlicing = false;
        Player.CanMove = true;

        if (Player.isGrounded && Input.GetButtonDown("Jump")) Player.Velocity2.y = Mathf.Sqrt(-2f * Player.Gravity * Player.JumpHeight);

    }




}
