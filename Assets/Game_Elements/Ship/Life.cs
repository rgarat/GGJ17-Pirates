using System;
using UnityEngine;
using System.Collections;


public class Life : MonoBehaviour {
	public int life = 100;

    [NonSerialized] public int maxLife;

	public float limitFall;
	public float incl;
	public float limitInc;
	public float limOffParticles;

	public float velFall;
	Vector3 pos;

	public bool vulnerable = false;
	public float percentageVul;
	private float timerToSafe;
	private int pressTimes;
	private float press;
	private float timer = 0;

	public GameObject[] particlesSmoke;
	[NonSerialized] public GameController gameController;


	void Start(){
		for (int i = 0; i < particlesSmoke.Length; i++) {
			var emitter = particlesSmoke [i].GetComponent<ParticleSystem> ().emission;
			emitter.enabled = false;
		}
		maxLife = life;

	}

	void Update(){
		
		if (life <= 0) {
			if (transform.position.y > pos.y - limitFall) {
				transform.Translate (Vector3.up * -velFall);
				if (transform.eulerAngles.z < limitInc) {
					transform.Rotate (Vector3.forward * incl);
				}
			} else {
				velFall = 0;
			}
			if (transform.position.y < pos.y - limOffParticles) {
				for (int i = 0; i < particlesSmoke.Length; i++) {
					var emitter = particlesSmoke [i].GetComponent<ParticleSystem> ().emission;
					emitter.enabled = false;
				}
			}
		}
	}

	public void life_rest (int damage) {
		
		if (vulnerable == true) {
			life -= damage*2;
		} else {
			life -= damage;
		}
		if (life < 50) {
			var emitter = particlesSmoke [0].GetComponent<ParticleSystem> ().emission;
			emitter.enabled = true;
		}
		if (life < 25) {
			var emitter = particlesSmoke [1].GetComponent<ParticleSystem> ().emission;
			emitter.enabled = true;
		}
		if (life < 10) {
			var emitter = particlesSmoke [2].GetComponent<ParticleSystem> ().emission;
			emitter.enabled = true;
		}


		if (life <= 0) {
			
			pos = gameObject.transform.position;
			GetComponent<Movement> ().enabled = false;
			GetComponent<Fire> ().enabled = false;
		}
	}

}
