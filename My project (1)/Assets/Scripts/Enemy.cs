using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform Player;

    public float CheckRadius, AttackSpeed;
    public LayerMask mask;


    enum state { Idle, Attack }
    state State;
    bool isFound;
    Transform Destination;


    // Start is called before the first frame update
    void Start()
    {

        State = state.Idle;
        Player = FindObjectOfType<Movement>().transform;
        Destination = FindObjectOfType<EnemyRoaming>().transform;
    }

    // Update is called once per frame
    void Update()
    {   ///Check for Player

        isFound = Physics.CheckSphere(transform.position, CheckRadius, mask);




        switch (State)
        {
            default:
            case state.Idle:
                FlyAround();


                break;

            case state.Attack:

                Attack();

                break;


        }





        ///if Found follow player

    }





    void FlyAround()
    {
        //Random position on the map to fly around
        StartCoroutine("Move");



    }


    void Attack()
    {

    }




    IEnumerator Move()
    {
        for (int i = 0; i < Destination.childCount; i++)
        {

            yield return new WaitForSeconds(1f);
            i++;

        }

        if (isFound)
        {
            State = state.Attack;
        }


    }











    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, CheckRadius);
    }


}
