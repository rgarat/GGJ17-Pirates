using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	[HideInInspector]
	public int damage;
	public float minY;
	public GameObject particleHit;
    public GameObject particleWater;

	void Update(){
		if (transform.position.y < minY)
		{
		    GameObject.Instantiate(particleWater, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}

	void OnTriggerEnter (Collider t){
	    if (t.gameObject.GetComponent<Life>() != null)
	    {
	        if (t.gameObject.tag != gameObject.tag)
	        {
	            Instantiate(particleHit, transform.position, transform.rotation);
	            t.gameObject.GetComponent<Life>().life_rest(damage);
	            Destroy(gameObject);
	        }
	    }
	    else
	    {
	        Instantiate(particleHit, transform.position, transform.rotation);
	        Destroy(gameObject);
	    }
	}
}
