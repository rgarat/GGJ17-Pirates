﻿using System.Collections;
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

    public PlayerWindow[] playerWindows;



    public Camera[] cameras;
    public Ocean ocean;

    public Rect[] cameraBounds;

    private Vector2[] hudMultiplier = new[]
    {
        new Vector2(1, 1),
        new Vector2(-1, 1),
        new Vector2(1, -1),
        new Vector2(-1, -1),
    };

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

	        var boatLife = go.GetComponent<Life>();

	        var playerWindow = this.playerWindows[i];
	        playerWindow.hud.life = boatLife;
	        playerWindow.hud.fire = boatFire;

	        var hudTransform = playerWindow.hud.GetComponent<RectTransform>();
	        var mainHudTransform = playerWindows[0].hud.GetComponent<RectTransform>();
	        var hudLocalPosition = mainHudTransform.localPosition;
	        hudLocalPosition.x *= hudMultiplier[i].x;
	        hudLocalPosition.y *= hudMultiplier[i].y;

	        hudTransform.localPosition = hudLocalPosition;

	        var hudScale = mainHudTransform.localScale;
	        hudScale.x *= hudMultiplier[i].x;
	        hudScale.y *= hudMultiplier[i].y;

	        hudTransform.localScale = hudScale;

	        go.tag = "Player" + (i + 1);
	    }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
