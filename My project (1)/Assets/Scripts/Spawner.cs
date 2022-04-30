using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] pos;


    // Update is called once per frame
    void Update()
    {
        int count = FindObjectsOfType<EnemyDrone>().Length;

        int randomNum = Random.Range(0, pos.Length - 1);

        if (count < 1)
        {
            Instantiate(enemy, pos[randomNum].position, Quaternion.identity);
        }

    }
}
