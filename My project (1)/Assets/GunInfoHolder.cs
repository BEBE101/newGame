using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new Gun", menuName = "GunInfo")]
public class GunInfoHolder : ScriptableObject
{

    public string gunName;
    public float xRecoil, yRecoil, zMove;



}
