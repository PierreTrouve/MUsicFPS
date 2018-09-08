using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour {

    public float radius = 30f;
    public GameObject player;

    // state 1 : pacific, 2 : satellites, 3 : final
    private int state = 1;

    EnemyManager enemyManager;
    MusicManager musicManager;
    public GameObject enemyPrefab;

    GameObject[] satelites = new GameObject[64];

    public void Init(MusicManager musicManagerParam) {
        EnemyManager[] enemyManagers = FindObjectsOfType(typeof(EnemyManager)) as EnemyManager[];
        enemyManager = enemyManagers[0];
        enemyManager.SubscribeBoss(this);
        musicManager = musicManagerParam;
    }

    void CreateEnemies(MusicManager musicManager) {
        for (int a = 0; a < 64; a++) {
            GameObject enemyInstance = (GameObject)Instantiate(enemyPrefab);

            //Subscribe the enemy to enemyManager
            EnemyBehavior enemyBehavior= enemyInstance.GetComponent<EnemyBehavior>();
            enemyManager.AddEnemy(enemyBehavior);

            // Subscribe the enemy to soundManader to animate them
            SampleTriggered soundTriggeredComponent = enemyInstance.GetComponent<SampleTriggered>();
            soundTriggeredComponent.Init(a);
            musicManager.SubscribeSampleSubscriber(soundTriggeredComponent);

            enemyInstance.transform.position = this.transform.position;
            enemyInstance.transform.parent = this.transform;

            this.transform.eulerAngles = new Vector3(0, -5.625f * a, 0);
            enemyInstance.transform.position = new Vector3((float)0f, (float)this.transform.position.y, (float)radius);
            satelites[a] = enemyInstance;
        }
    }

    public void Handle() {
        if (state == 1) {
            HandlePacific();
        } else if (state == 2) {
            HandleSatellitePhase();
        } else {
            HandleFinalPhase();
        }
    }

    public void AdvancePhase2() {
        if (state != 2) {
            state = 2;
            CreateEnemies(musicManager);
        }
    }

    public void AdvancePhase3() {
        state = 3;
    }

    void HandlePacific() {
        transform.position += new Vector3(0.01f, 0f, 0f);
    }


    void HandleSatellitePhase() {

    }


    void HandleFinalPhase() {

    }

    void Shoot() {

    }
}
