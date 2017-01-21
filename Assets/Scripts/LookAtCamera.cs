using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

	// Update is called once per frame
	void Update ()
	{
	    this.transform.position = target.position + offset;
	    this.transform.LookAt(target);
	}
}
