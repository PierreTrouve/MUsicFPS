using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : EnemyAbstract {

    public int lifePoint = 100;

    public override void Init() {

    }

    public override void Handle() {
        transform.position += new Vector3(0.1f, 0f, 0f);
    }
}
