using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public AudioClip ShootClip;
    
    private float kickIntensity, snareIntensity;
    public int baseDamage = 10;
    public int bonusDamage = 10;
    public float fireRate = 0.2f;

    public float weaponRange = 150f;
    public float hitForce = 100f;
    public Transform gunEnd;

    public LineRenderer[] linesRenderer = new LineRenderer[8];

    private Camera fpsCam;
    private WaitForSeconds shotDuration = new WaitForSeconds(0.2f);
    public TrackListenerScript trackListenerScript;

    private Animator animator;
    private AudioSource audioSource;


    // Use to tell next Time where shoot is allowed
    private float nextFire;

    public void Init() {
        //animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>();
        fpsCam = GetComponent<Camera>();
    }

    public void Fire()
    {
        if(!CanShoot()) return;

        int damageMeter = trackListenerScript.GetDamage();
        float musicDmg;
        switch (damageMeter) {
            case 3:
                musicDmg = 1f;
                break;
            case 2:
                musicDmg = 0.8f;
                break;
            case 1:
                musicDmg = 0.5f;
                break;
            default:
                musicDmg = 0f;
                break;
        }
        int dmg = baseDamage + (int)((float)bonusDamage * musicDmg);

        nextFire = Time.time + fireRate;
        

        for (int i = 0; i < Mathf.Pow(2, damageMeter); i++) {
            DrawShoot(i, dmg);
        }
             
    }

    void DrawShoot(int index, int dmg) {
        StartCoroutine(ShotEffect(index));
        Vector3 realDirection = fpsCam.transform.forward;
        Vector3 shootDirection = realDirection + new Vector3(
            Random.Range(-0.05f * index, 0.05f * index),
            Random.Range(-0.05f * index, 0.05f * index),
            Random.Range(-0.05f * index, 0.05f * index)
            );
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));
        RaycastHit hit;
        linesRenderer[index].SetPosition(0, gunEnd.position);
        if (Physics.Raycast(rayOrigin, shootDirection, out hit, weaponRange)) {
            linesRenderer[index].SetPosition(1, hit.point);
            Shootable shootableComponent = hit.collider.GetComponent<Shootable>();
            if (shootableComponent != null) {
                shootableComponent.Damage(dmg);
            }

            BossBehavior boosBehavior = hit.collider.GetComponent<BossBehavior>();
            if (boosBehavior != null) {
                boosBehavior.ReceiveDamage(dmg);
            }
        } else {
            linesRenderer[index].SetPosition(1, rayOrigin + (shootDirection * weaponRange));
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

    private IEnumerator ShotEffect(int index) {
        audioSource.PlayOneShot(ShootClip);
        linesRenderer[index].enabled = true;
        yield return shotDuration;
        linesRenderer[index].enabled = false;


    }
}
