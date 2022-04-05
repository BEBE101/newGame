using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public GameObject Bullet;
    public Transform Cam, Muzzle;


    public float fireRate, Damage;
    public bool isAutoGun;

    [Range(1, 100)]
    public int MagSize, bulletsPerShot;
    [Range(0, 1f)]
    public float timeBetweenShot;

    [Space]
    [Header("Recoil")]
    public GunInfoHolder gunInfo;


    Ray ray;
    GunSway recoilScript;
    int bulletsLeft;
    float xRotation;
    float yRotation, zMove;
    bool canShoot, isShooting, isReloading;

    // Start is called before the first frame update
    void Start()
    {


        ray = Camera.main.ViewportPointToRay(Cam.position);
        recoilScript = FindObjectOfType<GunSway>();

        bulletsLeft = MagSize;
        canShoot = true;

    }

    // Update is called once per frame
    void Update()
    {
        xRotation = gunInfo.xRecoil;
        yRotation = gunInfo.yRecoil;
        zMove = gunInfo.zMove;


        if (isAutoGun) isShooting = Input.GetKey(KeyCode.Mouse0);
        else isShooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (isShooting && bulletsLeft > 0 && !isReloading && canShoot)
        {
            StartCoroutine(Shooot());

        }


    }



    IEnumerator Shooot()
    {
        canShoot = false;
        Invoke("ResetShooting", fireRate);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject projectile = Instantiate(Bullet, Muzzle.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(Muzzle.right * 26000f * Time.fixedDeltaTime, ForceMode.Impulse);


            //Recoil
            Destroy(projectile, 15f);
            recoilScript.recoil(xRotation, yRotation, zMove);
            yield return new WaitForSecondsRealtime(timeBetweenShot);


        }


    }

    private void ResetShooting()
    { canShoot = true; }
}

