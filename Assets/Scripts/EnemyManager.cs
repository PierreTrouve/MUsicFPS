using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject boss;

    protected List<EnemyBehavior> enemies = new List<EnemyBehavior>();
    protected BossBehavior bossBehavior;

	// Use this for initialization
	public void Init (MusicManager musicManager) {
        bossBehavior = boss.GetComponent<BossBehavior>();
        bossBehavior.Init(musicManager);
    }

    public void AddEnemy(EnemyBehavior enemy) {
        enemies.Add(enemy);
    }

    public void SubscribeBoss(BossBehavior bossBehaviorParam) {
        bossBehavior = bossBehaviorParam;
    }

    // Update is called once per frame
    public void HandleEnnemies () {
        if (null != bossBehavior)
            bossBehavior.Handle();

		foreach(EnemyBehavior enemy in enemies) {
            enemy.Handle();
        }
	}

}
