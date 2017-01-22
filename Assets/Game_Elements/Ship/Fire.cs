using System;
using UnityEngine;
using System.Collections;
using XboxCtrlrInput;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour {

	public float shootForce;
	public int bulletDamage;

	public int shoots = 3;
	public float waitForShoot;
	public float chargeShoot;
	public float maxChargeShoot;
	public float minChargeShoot;
	float timer = 0;

	public int barrels;

	public GameObject bullet;
	public GameObject barrel;

	public GameObject fireParticle;

	public Transform[] cannons;
	public Transform barrelPointAdd;

    public XboxController controller;

    [NonSerialized] public GameController gameController;

	void Start(){
		shootForce = minChargeShoot;
	}

	void Update () {

	    if (gameController.paused)
	    {
	        return;
	    }

		if (XCI.GetButton(XboxButton.X, controller) && shoots > 0) {
			if (shootForce < maxChargeShoot) {
				shootForce = shootForce+chargeShoot;
			}
		}
		
		if (XCI.GetButtonUp(XboxButton.X, controller) && shoots > 0) {
			shoots--;
			for(int c = 0; c < cannons.Length; c ++) {
				if (cannons [c] != null) {
					if (fireParticle != null) {
						Instantiate (fireParticle, cannons [c].transform.position, cannons [c].transform.rotation);
					}
					GameObject b = Instantiate (bullet) as GameObject;
					b.gameObject.tag = gameObject.tag;
					b.transform.position = cannons [c].transform.position;
					b.transform.rotation = cannons [c].transform.rotation;
					float delayForce = Random.Range (0.8f,1.2f);
					b.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * shootForce * delayForce, ForceMode.Impulse);
					b.GetComponent<Bullet> ().damage = bulletDamage;
					Invoke ("ResetShootForce", 0.2f);
				}
			}
		}

		if (XCI.GetButtonDown(XboxButton.B, controller) && barrels > 0) {
			barrels--;
			Instantiate (barrel, barrelPointAdd.transform.position, barrelPointAdd.transform.rotation);
		}

		if (shoots < 3) {
			if (timer > waitForShoot) {
				timer = 0;
				shoots++;
			} else {
				timer += Time.deltaTime;
			}
		}

	}

	void ResetShootForce(){
		shootForce = minChargeShoot;
	}
}
