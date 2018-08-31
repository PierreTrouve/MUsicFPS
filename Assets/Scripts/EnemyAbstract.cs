using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour {

    public GameObject enemyManager;

    EnemyManager enemyManagerScript;

    void Start() {
        enemyManagerScript = enemyManager.GetComponent<EnemyManager>();
        enemyManagerScript.AddEnemy(this);
    }

    public abstract void Init();

    public abstract void Handle();
}
