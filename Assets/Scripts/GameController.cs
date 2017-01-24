using System.Collections;
using System.Collections.Generic;
using System.Linq;
using HUD;
using Pirates.Ocean;
using UnityEngine;
using UnityEngine.SceneManagement;
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

	public Texture[] shipsTextures;

    private Vector2[] hudPosMultiplier = new[]
    {
        new Vector2(1, 1),
        new Vector2(-1, 1),
        new Vector2(1, -1),
        new Vector2(-1, -1),
    };

    private Vector2[] hudScaleMultiplier = new[]
    {
        new Vector2(1, 1),
        new Vector2(-1, 1),
        new Vector2(1, 1),
        new Vector2(-1, 1),
    };


    public Sprite[] portraits;

    public bool paused;
    public GameObject optionsScreen;



    public GameObject krakenPrefab;
    public Transform[] krakenLocations;

    public float krakenRandomTime;


    private float krakenCounter = 0;

	// Use this for initialization
	void Start ()
	{
	    krakenCounter = krakenRandomTime;
	    cameras = new Camera[startPositons.Length];



	    for (int i = 0; i < startPositons.Length; i++)
	    {
	        var go = GameObject.Instantiate(boatPrefab, ocean.transform);
	        go.transform.localPosition = startPositons[i].localPosition;
	        var cameraGO = GameObject.Instantiate(cameraPrefab);
	        var lookAt = cameraGO.GetComponent<LookAtCamera>();
	        lookAt.target = go.transform;
	        lookAt.ocean = ocean;

			if (shipsTextures.Length > 0)
			{
			    var renderersToChange = go.GetComponentsInChildren<Renderer>()
			        .Where(renderer => renderer.sharedMaterial.mainTexture == shipsTextures[0])
			        .ToList();

			    var newMaterial = renderersToChange[0].material;
			    newMaterial.mainTexture = shipsTextures[i];
			    renderersToChange.ForEach(renderer1 => renderer1.material = newMaterial);


			}

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
	        hudLocalPosition.x *= hudPosMultiplier[i].x;
	        hudLocalPosition.y *= hudPosMultiplier[i].y;

	        hudTransform.localPosition = hudLocalPosition;

	        var hudScale = mainHudTransform.localScale;
	        hudScale.x *= hudScaleMultiplier[i].x;
	        hudScale.y *= hudScaleMultiplier[i].y;

	        hudTransform.localScale = hudScale;

	        go.tag = "Player" + (i + 1);

	        playerWindow.hud.SetPortrait(portraits[i]);

	        boatFire.gameController = this;
	        boatLife.gameController = this;
	        boatController.gameController = this;
	    }


	}



    

	// Update is called once per frame
    void Update()
    {
        if (XCI.GetButtonDown(XboxButton.Start, XboxController.All) || (paused && XCI.GetButtonDown(XboxButton.A, XboxController.All)) )
        {
            paused = !paused;
            Debug.LogFormat("Detected start: {0}", paused);
            optionsScreen.SetActive(paused);
            Time.timeScale = paused ? 0 : 1;
            return;
        }

        if (paused && XCI.GetButtonDown(XboxButton.B, XboxController.All))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }

        #if UNITY_EDITOR
        if (XCI.GetButton(XboxButton.Back))
        {
            foreach (var barco in GameObject.FindObjectsOfType<Movement>())
            {
                barco.velocidadMaxima = 10;
            }
        }
        #endif

        krakenCounter -= Time.deltaTime;
        if (krakenCounter < 0)
        {
            //create kraken
            var kraken = GameObject.Instantiate(krakenPrefab, ocean.transform);
            var location = krakenLocations[UnityEngine.Random.Range(0, krakenLocations.Length)].localPosition;
            kraken.transform.localPosition = location;


            krakenCounter = UnityEngine.Random.Range(0.8f * krakenRandomTime, 1.2f * krakenRandomTime);

        }
    }


}
