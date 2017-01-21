using System.Collections;
using System.Collections.Generic;
using Pirates.Ocean;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject boatPrefab;
    public Camera cameraPrefab;
    public Transform[] startPositons;


    public Camera[] cameras;
    public Ocean ocean;

    private Rect[] cameraBounds = new Rect[]
    {
        new Rect(0, 0, 0.5f, 0.5f),
        new Rect(0, 0.5f, 0.5f, 0.5f),
        new Rect(0.5f, 0.5f, 0.5f, 0.5f),
        new Rect(0.5f, 0f, 0.5f, 0.5f),
    };


	// Use this for initialization
	void Start ()
	{
	    for (int i = 0; i < startPositons.Length; i++)
	    {
	        var go = GameObject.Instantiate(boatPrefab, ocean.transform);
	        go.transform.localPosition = startPositons[i].localPosition;
	        var cameraGO = GameObject.Instantiate(cameraPrefab);
	        var lookAt = cameraGO.GetComponent<LookAtCamera>();
	        lookAt.target = go.transform;

	        var camera = cameraGO.GetComponent<Camera>();
	        camera.rect = cameraBounds[i];
	    }

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
