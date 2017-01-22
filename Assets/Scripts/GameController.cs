using System.Collections;
using System.Collections.Generic;
using HUD;
using Pirates.Ocean;
using UnityEngine;
using XboxCtrlrInput;

public class GameController : MonoBehaviour
{

    public GameObject boatPrefab;
    public Camera cameraPrefab;
    public Transform[] startPositons;

    public BoatHud[] huds;



    public Camera[] cameras;
    public Ocean ocean;

    public Rect[] cameraBounds;


	// Use this for initialization
	void Start ()
	{
	    cameras = new Camera[startPositons.Length];


	    for (int i = 0; i < startPositons.Length; i++)
	    {
	        var go = GameObject.Instantiate(boatPrefab, ocean.transform);
	        go.transform.localPosition = startPositons[i].localPosition;
	        var cameraGO = GameObject.Instantiate(cameraPrefab);
	        var lookAt = cameraGO.GetComponent<LookAtCamera>();
	        lookAt.target = go.transform;

	        var camera = cameraGO.GetComponent<Camera>();
	        camera.rect = cameraBounds[i];

	        var boatController = go.GetComponent<Movement>();
	        boatController.controller = XboxController.First + i;
	        var boatFire = go.GetComponent<Fire>();
	        boatFire.controller = boatController.controller;

	        var hud = huds[i];
	        hud.life = go.GetComponent<Life>();

	        go.tag = "Player" + (i + 1);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
