using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public AudioClip snareSuccessHitClip, snareCriticalHitClip, snareFailedHitClip;
    
    public float kickIntensity, snareIntensity;

    public int snareBaseDamage = 10;
    public int snareBonusDamage = 10;

    public int kickBaseDamage = 10;
    public int kickBonusDamage = 10;

    public float criticalThreshold = 0.9f;
    public float failedThreshold = 0.1f;


    public float failedFireRate = 1f;
    public float successFireRate = 0.3f;
    public float criticalFireRate = 0.1f;


    public int comboMeter = 0;

    private Animator animator;
    private AudioSource audioSource;


    // Use to tell next Time where shoot is allowed
    private float nextFire;

    public void Init() {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SnareShoot()
    {
        if(!CanShoot()) return;
        int dmg = snareBaseDamage + (int)((float)snareBonusDamage * snareIntensity);

        if (snareIntensity > criticalThreshold) {
            HandleCriticalShot();

        } else if (snareIntensity > failedThreshold) {
            HandleNormalShot();
        } else {
            HandleFailedShot();
        }
    }

    private void HandleCriticalShot() {
        comboMeter++;
        nextFire = Time.time + criticalFireRate;
        animator.SetTrigger("CriticalShoot");
        audioSource.PlayOneShot(snareCriticalHitClip);
    }
    
    private void HandleNormalShot() {
        comboMeter++;
        nextFire = Time.time + successFireRate;
        animator.SetTrigger("Shoot");
        audioSource.PlayOneShot(snareSuccessHitClip);

    }

    private void HandleFailedShot() {
        comboMeter = 0;
        animator.SetTrigger("MissedShoot");
        nextFire = Time.time + failedFireRate;
        audioSource.PlayOneShot(snareFailedHitClip);

    }

    public void KickShoot()
    {
        if (!CanShoot()) return;
        int dmg = kickBaseDamage + (int)((float)kickBonusDamage * kickIntensity);

        float fireRate = 0;
        if (kickIntensity > criticalThreshold) {
            comboMeter++;
            fireRate = successFireRate;
        } else {
            fireRate = failedFireRate;
            comboMeter = 0;
        }
        nextFire = Time.time + fireRate;

    }

    bool CanShoot()
    {
        return Time.time > nextFire;
    }

    public void SetIntensities(float newKickIntensity, float newSnareIntensity) {
        kickIntensity = newKickIntensity;
        snareIntensity = newSnareIntensity;
    }
}
