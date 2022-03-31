using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomForce : MonoBehaviour
{
    Rigidbody rb;
    float speed = 1200f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * Random.Range(-speed, speed) * Time.fixedDeltaTime, ForceMode.Impulse);
        rb.AddForce(transform.up * Random.Range(-speed, speed) * Time.fixedDeltaTime, ForceMode.Impulse);
        rb.AddForce(transform.forward * Random.Range(-speed, speed) * Time.fixedDeltaTime, ForceMode.Impulse);

    }
    // Update is called once per frame



}
