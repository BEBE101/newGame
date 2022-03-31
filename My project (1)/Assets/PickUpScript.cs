using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpScript : MonoBehaviour
{

    public float lerpTime;
    public Transform cam;


    public LayerMask pickUpLayer;


    bool isInRange, slotFilled;
    Transform _pickedObj;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {




        if (slotFilled)
        {
            _pickedObj.localPosition = Vector3.Lerp(_pickedObj.localPosition, Vector3.zero, lerpTime * Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                slotFilled = false;
            }
        }

        if (!slotFilled)
        {
            if (_pickedObj != null)
                _pickedObj.GetComponent<Rigidbody>().isKinematic = false; transform.DetachChildren();


        }


    }





}
