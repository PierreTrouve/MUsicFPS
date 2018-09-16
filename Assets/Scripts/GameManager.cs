using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public MusicManager musicManager;
    public WeaponManager weaponManager;
    public EnemyManager enemyManager;
    public TrackListenerScript trackManager;

	// Use this for initialization
	void Start () {
        musicManager.Init();
        weaponManager.Init();
        enemyManager.Init(musicManager);
        enemyManager.StartPhase2();

        
    }
	
	// Update is called once per frame
	void Update () {
        musicManager.ComputeIntensities();
        weaponManager.SetIntensities(musicManager.kickIntensity, musicManager.snareIntensity);

        if (Input.GetButtonDown("Fire1")) {
            weaponManager.Fire();
        } 

        if (Input.GetKeyDown("o")) {
            musicManager.StartMusic();
            trackManager.StartLecture();
        } 

        enemyManager.HandleEnnemies();
    }
}
