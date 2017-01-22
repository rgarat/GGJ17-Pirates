using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	[HideInInspector]
	public int damage;
	public float minY;
	public GameObject particleHit;

	void Update(){
		if (transform.position.y < minY) {
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider t){
		if (t.gameObject.GetComponent<Life>() != null && t.gameObject.tag != gameObject.tag) {
			Instantiate (particleHit, transform.position, transform.rotation);
			t.gameObject.GetComponent<Life> ().life_rest(damage);
			Destroy (gameObject);
		}
	}
}
