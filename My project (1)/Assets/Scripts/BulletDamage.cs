using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public LayerMask enemyLayers;
    public float range;

    RaycastHit hit;
    public int damageValue;


    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Drone"))
        {
            collision.gameObject.GetComponent<EnemyDrone>().TakeDamage(damageValue);

        }
        Destroy(gameObject, 0.001f);
    }
}
