using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    protected List<EnemyAbstract> enemies = new List<EnemyAbstract>();

	// Use this for initialization
	public void Init () {
		
	}

    public void AddEnemy(EnemyAbstract enemy) {
        enemies.Add(enemy);
    }
	
	// Update is called once per frame
	public void HandleEnnemies () {
		foreach(EnemyAbstract enemy in enemies) {
            enemy.Handle();
        }
	}
}
