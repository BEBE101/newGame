using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public GameObject Bullet;
    public Transform Cam, Muzzle;


    public float fireRate;
    public int Damage;
    public bool isAutoGun;

    [Range(1, 100)]
    public int MagSize, bulletsPerShot;
    [Range(0, 1f)]
    public float timeBetweenShot;

    [Space]
    [Header("Recoil")]
    public GunInfoHolder gunInfo;
    public Transform _gunBarrel;
    public float barrelMovePos;

    [Space]
    public ParticleSystem MuzzleFlash;


    Ray ray;
    GunSway recoilScript;
    int bulletsLeft;
    float xRotation;
    float yRotation, zMove;
    bool canShoot, isShooting, isReloading;

    // Start is called before the first frame update
    void Start()
    {

        MuzzleFlash.Stop();
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
            canShoot = false;
            StartCoroutine(Shooot());

        }


    }



    IEnumerator Shooot()
    {
        MuzzleFlash.Play();
        Invoke("ResetShooting", fireRate);

        for (int i = 0; i < bulletsPerShot; i++)
        {
            GameObject projectile = Instantiate(Bullet, Muzzle.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(Muzzle.right * 13000f * Time.fixedDeltaTime, ForceMode.VelocityChange);
            projectile.GetComponent<BulletDamage>().damageValue = Damage;


            //Recoil
            Destroy(projectile, 15f);
            recoilScript.recoil(xRotation, yRotation, zMove);
            _gunBarrel.LeanMoveLocalZ(barrelMovePos, 0.02f).setOnComplete(vruhj);
            yield return new WaitForSecondsRealtime(timeBetweenShot);


        }


    }

    void vruhj()
    {
        _gunBarrel.LeanMoveLocalZ(0, 0.1f).setEaseInBack();
    }


    private void ResetShooting()
    { canShoot = true; }
}

