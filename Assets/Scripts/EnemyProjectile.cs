using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

    Vector3 targetPosition;
    bool activated = false;
    public float velocity = 1f;

    Vector3 directionStep;
    private WaitForSeconds explosionDuration = new WaitForSeconds(.07f);


    public void Init(Vector3 target) {
        targetPosition = target;
        activated = true;
        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        directionStep = direction * velocity;

        SampleTriggered sampleTriggered = GetComponent<SampleTriggered>();
        sampleTriggered.activated = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (activated) {
            transform.position += directionStep;
        }
	}


    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            return;
        }

        activated = false;
        if (other.gameObject.tag == "Player") {
            PlayerBehavior playerBehavior = other.gameObject.GetComponentInChildren<PlayerBehavior>();
            playerBehavior.ReceiveDamage(8);
        }
        StartCoroutine(ExplodeEffect());

        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;

        particleSystem.Play();

    }

    private IEnumerator ExplodeEffect() {
        yield return explosionDuration;
        gameObject.SetActive(false);

    }


}
