using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Kraken : MonoBehaviour {
	public Transform kraken;
	public GameObject[] tentacles;
	private GameObject ship;

	public float timerToSafe;
	public int pressTimes;

	public float krakenY;

	private bool active = false;
	private float timer = 0;
	private int press=0;

	public XboxController controller;

    public float timeToDissapear;

	void Start () {
		
	}

	void Update () {
	    if (active == true)
	    {
	        timer += Time.deltaTime;
	        if (timer <= timerToSafe)
	        {
	            if (XCI.GetButtonDown(XboxButton.A, XboxController.All))
	            {
	                press++;
	            }
	        }
	        if (press >= pressTimes)
	        {
	            active = false;
	            KrakenFail();
	        }
	        if (timer > timerToSafe)
	        {
	            active = false;
	            if (press < pressTimes)
	            {
	                ship.GetComponent<Life>().life_rest(100);
	                for (int count = 0; count < tentacles.Length; count++)
	                {
	                    tentacles[count].GetComponent<Animator>().Play("Anim_Kraken_Win");
	                }
	            }
	        }
	    }
	    else
	    {
	        timer += Time.deltaTime;
	        if (timer > timeToDissapear)
	        {
	            GameObject.Destroy(this.gameObject);
	        }
	    }
	}

	void Attack(){
		for (int count = 0; count < tentacles.Length; count++) {
			tentacles [count].GetComponent<Animator> ().Play ("Anim_Kraken_Start");
		}
		active = true;
	}

	void KrakenFail(){
		ship.GetComponent<Fire> ().enabled = true;
		ship.GetComponent<Movement> ().enabled = true;
		for (int count = 0; count < tentacles.Length; count++) {
			tentacles [count].GetComponent<Animator> ().Play ("Anim_Kraken_Dead");
		}
	}

	public void Dead(){
		for (int count = 0; count < tentacles.Length; count++) {
			tentacles [count].GetComponent<Animator> ().Play ("Anim_Kraken_Dead");
		}
	}

	public void DestroyKraken(){
		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider t){
		if (t.gameObject.GetComponent<Life> () != null) {
			timer = 0;
			press = 0;
			ship = t.gameObject;
			kraken.position = new Vector3 (ship.transform.position.x, ship.transform.position.y-krakenY, ship.transform.position.z);
			ship.GetComponent<Rigidbody> ().velocity = new Vector3(0,0,0);
			ship.GetComponent<Movement> ().enabled = false;
			controller = t.gameObject.GetComponent<Movement> ().controller;
			ship.GetComponent<Fire> ().enabled = false;
			Attack ();
		}
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(this.transform.position, 2);
    }
}
