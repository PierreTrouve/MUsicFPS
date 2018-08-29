using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public MusicManager musicManager;
    public WeaponManager weaponManager;

	// Use this for initialization
	void Start () {
        musicManager.Init();
        weaponManager.Init();
	}
	
	// Update is called once per frame
	void Update () {
        musicManager.ComputeIntensities();
        weaponManager.SetIntensities(musicManager.kickIntensity, musicManager.snareIntensity);

        if (Input.GetButtonDown("Fire1")) {
            weaponManager.SnareShoot();
        } else if(Input.GetButtonDown("Fire2")) {
            weaponManager.KickShoot();
        }

    }
}
