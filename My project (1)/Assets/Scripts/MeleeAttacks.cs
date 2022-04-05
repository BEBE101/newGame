using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacks : MonoBehaviour
{
    //PowerSlice
    public Transform SlicePoint, Cam;
    public float SliceSpeed, SliceDis, SliceMaxTime, SliceDamage;
    public bool isSlicing;


    [Header("GroundSlam")]
    [Space]
    public float MinDamage;
    public float MaxDamage;

    [Range(0, 200f)]
    public float SlamSpeed, SlamSpeedLerpTime, SlamStopTime;
    public bool isSlamingGround;
    float GroundDis;



    [Space]
    [Space]
    public GameObject SlamPart;
    public ParticleSystem FastParticle;
    public Camera cam;
    public float FoVChange, LerpTime;



    [Header("Cube Throw")]
    public float _throwForce;
    public float _throwRate;
    public GameObject Cube;
    public Transform PickObj;


    ParticleSystem SlamParticle;
    Movement Player;
    static float time, damage, dis, InitialFoV;
    bool canThrowCube;



    private void Awake()
    {
        SlamParticle = SlamPart.GetComponent<ParticleSystem>();
        InitialFoV = FindObjectOfType<CamMove>().InitialFoV;
        Player = FindObjectOfType<Movement>();
        FastParticle.Stop();
        canThrowCube = true;

    }
    private void Update()
    {
        ThrowCube();




        if (Player.isGrounded) Slice();
        //  else GroundSlam();

        if (isSlicing) SliceMove(time);
        else return;
        fovFX();


    }


    void Slice()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            SlicePoint.localPosition = new Vector3(0, 0, SliceDis);
            canThrowCube = false;
            isSlicing = true;
            time = Time.time + SliceMaxTime;


        }




    }

    void SliceMove(float t)
    {
        //Distance
        float dis = Vector3.Distance(SlicePoint.position, transform.position);
        float Sped = Mathf.Clamp(dis, 10f, 50f);


        //Motion
        Vector3 move = (SlicePoint.position - transform.position).normalized;
        Player.CC.Move(move * Sped * SliceSpeed * Time.deltaTime);


        //Particles



        //Stopping
        if (t < Time.time)
        {
            isSlicing = false;
            LastMove();

        }
    }


    void LastMove()
    {
        if (!isSlicing)
        {
            float sped = 5f;
            sped = sped - 3f * Time.deltaTime;
            sped = Mathf.Clamp(sped, 0, Mathf.Infinity);
            Player.Velocity = transform.forward * sped;
            canThrowCube = true;

            if (sped < 0.5f) CancelInvoke("LastMove"); FastParticle.Stop();

        }
    }

    /*

    void GroundSlam()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isSlamingGround = true;
            canThrowCube = false;
            RaycastHit hit;
            Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
            GroundDis = hit.distance;
        }


        if (isSlamingGround) SlamMove();

    }
    void SlamMove()
    {
        float speed = Mathf.Lerp(0, SlamSpeed, SlamSpeedLerpTime * Time.deltaTime);
        time = Time.time + 2f;
        Player.CC.Move(Vector3.down.normalized * 10f * speed * Time.deltaTime);
        Player.CanMove = false;


        //DistanceCheck
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity);
        float dis = hit.distance;


        if (dis < 1.4f || time > Time.time)
        {
            StartCoroutine(SlamFx());

            Invoke("StopMotion", SlamStopTime);



        }

    }

    void StopMotion()
    {
        isSlamingGround = false;
        isSlicing = false;
        Player.CanMove = true;







    }
    IEnumerator SlamFx()
    {
        GameObject part = Instantiate(SlamPart, transform);
        part.transform.localPosition = new Vector3(0, -0.8f, 0);
        SlamParticle = part.GetComponent<ParticleSystem>();
        StartCoroutine(SlamCamMove());
        yield return new WaitForSeconds(0.002f);

        part.transform.SetParent(null);
        SlamParticle.Play();
        yield return new WaitForSeconds(0.1f);

        canThrowCube = true;
        SlamParticle.Stop();
        Destroy(part, 0.2f);

    }
    IEnumerator SlamCamMove()
    {
        LeanTween.moveLocalY(Cam.gameObject, Cam.localPosition.y - 0.2f, 0.3f).setEaseInBounce();
        yield return new WaitForSeconds(0.3f);
        LeanTween.moveLocalY(Cam.gameObject, 0.52f, 0.5f).setEaseInBounce();
    }

    */

    void fovFX()
    {
        if (isSlicing)
        {
            cam.fieldOfView = LeanTween.easeOutExpo(cam.fieldOfView, FoVChange, LerpTime * Time.deltaTime);
            FastParticle.Play();

        }
        else FastParticle.Stop();

    }







    void ThrowCube()
    {

        float Rate = 0f;




        if (canThrowCube && Input.GetKeyDown(KeyCode.T) && Rate < Time.time)
        {
            GameObject projectile = Instantiate(Cube, PickObj.position, Quaternion.identity);
            Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();

            projectileRB.AddForce(Cam.forward * _throwForce * 30f * Time.fixedDeltaTime, ForceMode.Impulse);
            projectileRB.AddRelativeTorque(Cam.forward * Random.Range(-_throwForce, _throwForce) * Time.deltaTime, ForceMode.Impulse);
            canThrowCube = false;
            Destroy(projectile, 5f);
            Invoke("ResetThrow", _throwRate);
        }

    }
    void ResetThrow() { canThrowCube = true; }


}
