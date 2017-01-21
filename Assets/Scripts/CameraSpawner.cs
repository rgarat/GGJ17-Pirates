using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour
{

    public GameObject camera;

	void Start ()
	{

	    var go = GameObject.Instantiate(camera);
	    var lookAt = go.GetComponent<LookAtCamera>();
	    lookAt.target = this.gameObject.transform;

	}
	

}
