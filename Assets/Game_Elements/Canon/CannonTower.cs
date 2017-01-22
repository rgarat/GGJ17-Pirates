using UnityEngine;
using System.Collections;
using System.Linq;

public class CannonTower : MonoBehaviour {
	
	Movement[] ships;
	GameObject targetShip;

	public GameObject[] cannons;

	public GameObject bullet;

	public int bulletDamage;

	public float timeShoot;

	public float MaxDistance;

	public float maxChangeShoot;
	public float minChargeShoot;

	public float shootForce;

	void Start () {
		
		Invoke ("SearchShips", 1f);

		//Linea Rara XD (Estudiar)
		//GameObject[] sj2 = GameObject.FindObjectsOfType<Movement> ().Select ((Movement arg) => arg.gameObject).ToArray ();

	}

	void SearchShips(){
		ships = GameObject.FindObjectsOfType<Movement>();
		InvokeRepeating ("SearchNearShip", timeShoot, timeShoot);
	}

	void SearchNearShip(){
		return;
		for (int count = 0; count < ships.Length; count++) {
			
			var go = ships [count].gameObject;
			float distance = Vector3.Distance(go.transform.position, transform.position);

			if(distance <= MaxDistance){
				//shoot
				for (int c = 0; c < cannons.Length; c++) {
					shootForce = Random.Range (minChargeShoot, maxChangeShoot);
					GameObject b = Instantiate (bullet) as GameObject;
					//b.gameObject.tag = gameObject.tag;
					b.transform.position = cannons [c].transform.position;
					b.transform.rotation = cannons [c].transform.rotation;
					//float delayForce = Random.Range (0.8f,1.2f);
					//b.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * shootForce * delayForce, ForceMode.Impulse);
					b.GetComponent<Rigidbody> ().AddRelativeForce (Vector3.forward * shootForce, ForceMode.Impulse);
					b.GetComponent<Bullet> ().damage = bulletDamage;

				}
			}
		}
	}

}
