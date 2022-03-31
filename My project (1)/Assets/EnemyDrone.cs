using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrone : MonoBehaviour
{

    public float _speed, _speedMultiplier, StopTimeWhenHit, MaxHeight;
    public float _MovementSnapiness;

    [Space]
    public float RotationSlerp, PlayerCheckRadius;
    public int Health;
    public Rigidbody rb;


    [Space]
    public LayerMask PlayerLayer;
    public Transform[] MovePlaces;


    public enum state { Attack, Dead }
    state State;

    [Space]
    public float _propellerSpeed;
    public Material _eyeMat;
    public ParticleSystem _floatingParticles, _floatingParticles2;


    Transform DroneChild, Player;
    Vector3 SampleMove, Move;
    bool isFound, canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        State = state.Attack;
        Player = FindObjectOfType<Movement>().transform;
        DroneChild = transform.GetChild(0);

    }

    // Update is called once per frame
    void Update()
    {







        Vector3 offset = new Vector3(0, -90f, 0f);
        DroneChild.localRotation = Quaternion.Slerp(DroneChild.localRotation, Quaternion.Euler(offset), RotationSlerp * Time.deltaTime);


        //getting direction and smoothing the movement;
        Move = Vector3.Lerp(Move, Player.position, Time.deltaTime * _MovementSnapiness);
        SampleMove = (Move - transform.position).normalized;





        switch (State)
        {
            default:
            case state.Attack:
                AttackPlayer();
                break;
            case state.Dead:
                break;

        }



    }





    void AttackPlayer()
    {
        float Dis = Vector3.Distance(Player.position, transform.position);


        if (canMove)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position).normalized, 5f * Time.deltaTime);

            //Fx
            _propellerSpeed = 400f;
            _floatingParticles.Play();
            _floatingParticles2.Play();


            if (Dis > 5f) rb.velocity = SampleMove * _speed * _speedMultiplier * 20f * Time.deltaTime;

            if (!Physics.Raycast(transform.position, Vector3.down, MaxHeight)) rb.useGravity = true;
            else { rb.useGravity = false; }
        }
        else
        {
            //Fx
            _propellerSpeed = Mathf.Lerp(_propellerSpeed, 0, 15f * Time.deltaTime);
            _floatingParticles.Stop();
            _floatingParticles2.Stop();
        }






    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PickUpObj"))
        {
            //stops following player
            canMove = false;

            //adding gravity
            rb.useGravity = true;

            ///Adding random rotation when hit with cube
            rb.AddTorque(transform.up * Random.Range(-400f, 400f) * 2000f * Time.deltaTime, ForceMode.Impulse);
            rb.AddTorque(transform.forward * Random.Range(-400f, 400f) * 2000f * Time.deltaTime, ForceMode.Impulse);
            rb.AddTorque(transform.right * Random.Range(-400f, 400f) * 2000f * Time.deltaTime, ForceMode.Impulse);


            //starts to follow player after stop Timer
            Invoke("Reset", StopTimeWhenHit);
        }
    }
    private void Reset()
    {
        //Starts to follow Player
        canMove = true;
        rb.useGravity = false;
        LeanTween.moveY(this.gameObject, transform.position.y + 2f, 0.7f).setEaseOutCubic();
    }



}
