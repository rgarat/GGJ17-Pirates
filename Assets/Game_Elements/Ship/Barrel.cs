using UnityEngine;
using System.Collections;

public class Barrel : MonoBehaviour {
	public float activationTime;
	public int damage;
	public GameObject particleExplosion;

	void Update () {
		if(activationTime > 0){
			activationTime -= Time.deltaTime;
		}
	}
	void OnCollisionEnter (Collision c){
		if (c.gameObject.GetComponent<Life>() != null && activationTime<=0) {
			c.gameObject.GetComponent<Life> ().life_rest(damage);
			if(particleExplosion != null){
				Instantiate (particleExplosion, transform.position, transform.rotation);
			}
			Destroy (gameObject);
		}
	}
}
