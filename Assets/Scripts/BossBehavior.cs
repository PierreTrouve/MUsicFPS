using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour {

    public float radius = 30f;

    EnemyManager enemyManager;
    public GameObject enemyPrefab;

    GameObject[] satelites = new GameObject[64];

    public void Init(MusicManager musicManager) {
        EnemyManager[] enemyManagers = FindObjectsOfType(typeof(EnemyManager)) as EnemyManager[];
        enemyManager = enemyManagers[0];
        enemyManager.SubscribeBoss(this);

        CreateEnemies(musicManager);
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
        transform.position += new Vector3(0.01f, 0f, 0f);
    }
}
