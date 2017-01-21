using System.Collections;
using System.Collections.Generic;
using Pirates.Ocean;
using UnityEngine;

public class WaveFollow : MonoBehaviour
{

    public Ocean ocean;
    public float yOffset;
    public GameObject boatGraphic;


    public void Start()
    {
        if (ocean == null)
        {
            this.ocean = this.GetComponentInParent<Ocean>();
        }
    }
	
	// Update is called once per frame
	void LateUpdate ()
	{
	    var pos = this.transform.localPosition;
	    pos.y = 0;
	    pos = ocean.GetWithHeight(pos);


	    boatGraphic.transform.localPosition = new Vector3(0, pos.y + yOffset, 0);
	}
}
