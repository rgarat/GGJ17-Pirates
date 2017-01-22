using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken_Arm : MonoBehaviour {
	public bool active = false;
	public GameObject kraken;
	public void Dead() {
		if (active == true) {
			kraken.GetComponent<Kraken> ().DestroyKraken ();
		}
	}
}
