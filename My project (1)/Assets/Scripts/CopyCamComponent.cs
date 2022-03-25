using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCamComponent : MonoBehaviour
{
    public Camera Main;
    Camera self;

    private void Start()
    {
        self = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        self.fieldOfView = Main.fieldOfView;
        self.allowHDR = Main.allowHDR;
        self.focalLength = Main.focalLength;
    }
}
