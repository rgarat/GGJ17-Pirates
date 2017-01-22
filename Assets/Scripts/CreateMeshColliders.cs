using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMeshColliders : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    var meshFilters = this.GetComponentsInChildren<MeshFilter>();

	    foreach (var mesh in meshFilters)
	    {
	        var meshCollider = mesh.gameObject.AddComponent<MeshCollider>();
	        meshCollider.sharedMesh = mesh.sharedMesh;
	    }
	}
	

}
