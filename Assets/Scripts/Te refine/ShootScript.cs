using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = .25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private AudioSource gunAudio;
    private LineRenderer laserLine;
    private float nextFire;

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        fpsCam = GetComponentInParent<Camera>();
    }
	
	// Update is called once per frame
	void Update () {


        bool shootgunPressed = Input.GetButtonDown("Fire1");
        bool lazerGunPressed = Input.GetButtonDown("Fire2");

        if (shootgunPressed)
        {
            int shootgunDmg = 10;

            Debug.Log("Shootgun : " + shootgunDmg + " dgs");
        }

        if (lazerGunPressed && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            StartCoroutine(ShotEffect());
            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            RaycastHit hit;

            laserLine.SetPosition(0, gunEnd.position);

            if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
                laserLine.SetPosition(1, hit.point);
                Shootable health = hit.collider.GetComponent<Shootable>();

                if (health != null)
                {
                    health.Damage((int)(1 * 400));
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce);
                }
            }
            else
            {
                laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }

            int lazerDmg = (int)(1 * 400);
            Debug.Log("Lazer : " + lazerDmg + " dmg");
        }



    }

    private IEnumerator ShotEffect()

    {

        gunAudio.Play();
        laserLine.enabled = true;
        yield return shotDuration;

        laserLine.enabled = false;

    }
}
