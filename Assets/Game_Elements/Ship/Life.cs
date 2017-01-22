using System;
using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {
	public int life = 100;

    [NonSerialized] public int maxLife;

	public float limitFall;
	public float incl;
	public float limitInc;

	public float velFall;
	Vector3 pos;

    [NonSerialized] public GameController gameController;

    void Start()
    {
        maxLife = life;
    }

	void Update(){
		if (life <= 0) {
			if (transform.position.y > pos.y-limitFall) {
				transform.Translate (Vector3.up*-velFall);
				if (transform.eulerAngles.z < limitInc) {
					transform.Rotate (Vector3.forward * incl);
				}
			} else {
				velFall = 0;
			}
		}
	}

	public void life_rest (int damage) {
		life -= damage;
		if (life <= 0) {
			pos = gameObject.transform.position;
			GetComponent<Movement> ().enabled = false;
			GetComponent<Fire> ().enabled = false;
		}
	}
}
