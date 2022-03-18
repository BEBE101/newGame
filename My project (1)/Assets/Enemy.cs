using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    Transform Player;
    public NavMeshAgent navAgent;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<Movement>().transform;
    }

    // Update is called once per frame
    void Update()
    {

        navAgent.SetDestination(Player.position);
    }
}
