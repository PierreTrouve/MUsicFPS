using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public AudioClip ShootClip;
    
    private float kickIntensity, snareIntensity;
    public int baseDamage = 10;
    public int bonusDamage = 10;
    public float fireRate = 0.2f;

    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Transform gunEnd;

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer laserLine;

    private Animator animator;
    private AudioSource audioSource;


    // Use to tell next Time where shoot is allowed
    private float nextFire;

    public void Init() {
        //animator = GetComponent<Animator>();

        laserLine = GetComponent<LineRenderer>();
        audioSource = GetComponent<AudioSource>();
        fpsCam = GetComponent<Camera>();
    }

    public void Fire()
    {
        if(!CanShoot()) return;
        int dmg = baseDamage + (int)((float)bonusDamage * snareIntensity);
        Debug.Log(dmg); 
        nextFire = Time.time + fireRate;
        StartCoroutine(ShotEffect());
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

        RaycastHit hit;

        laserLine.SetPosition(0, gunEnd.position);
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange)) {
            laserLine.SetPosition(1, hit.point);
            Shootable shootableComponent = hit.collider.GetComponent<Shootable>();
      
            if (shootableComponent != null)
            {
                shootableComponent.Damage(dmg);
            }
        } else {
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }

    bool CanShoot()
    {
        return Time.time > nextFire;
    }

    public void SetIntensities(float newKickIntensity, float newSnareIntensity) {
        kickIntensity = newKickIntensity;
        snareIntensity = newSnareIntensity;
    }

    private IEnumerator ShotEffect() {
        audioSource.PlayOneShot(ShootClip);
        laserLine.enabled = true;
        yield return shotDuration;
        laserLine.enabled = false;


    }
}
