using System.Collections;
using System.Collections.Generic;
using Pirates.Ocean;
using UnityEditor;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;
    public Ocean ocean;

    private Vector3 lastTargetPos;

	// Update is called once per frame
	void Update ()
	{
	    var targetPosition = target.localPosition;
	    targetPosition = ocean.CameraBounds.ClosestPoint(targetPosition);
	    targetPosition = target.transform.parent.localToWorldMatrix.MultiplyPoint3x4(targetPosition);

	    this.transform.position = targetPosition + offset;
	    this.transform.LookAt(targetPosition);
	    lastTargetPos = targetPosition;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(lastTargetPos, 2);
    }
}
