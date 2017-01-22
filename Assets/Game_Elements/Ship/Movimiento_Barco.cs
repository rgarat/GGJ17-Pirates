using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class Movimiento_Barco : MonoBehaviour {
	
	public float velocidadMaxima;
	public float aceleracion;
	public float giro;
	public float velocidadGiroMovimiento;
	public float velocidadGiroQuieto;
	public float factorRozamientoAgua;

	private float rozamientoAgua;
	private float velocidad;

	public KeyCode botonAcelerar;
	public KeyCode botonDerecha;
	public KeyCode botonIzquierda;

	private Rigidbody rig;

    public int playerNumber;
    public XboxController controller;

	void Start () {
		rig = GetComponent<Rigidbody> ();
	}



	void Update () {


	    if (XCI.GetButton(XboxButton.A, controller))
	    {


		//if (Input.GetButton (inputPrefix + "_Action1")) {
			rig.AddRelativeForce (Vector3.forward * aceleracion, ForceMode.Acceleration);
			rig.velocity = Vector3.ClampMagnitude (rig.velocity, velocidadMaxima);
		}

		//Vector3 targetDir = new Vector3 (Input.GetAxis (inputPrefix + "_Horizontal"), 0, -Input.GetAxis (inputPrefix + "_Vertical"));
		Vector3 targetDir = new Vector3 (XCI.GetAxis(XboxAxis.LeftStickX, controller), 0, XCI.GetAxis(XboxAxis.LeftStickY, controller));

		if (targetDir.magnitude > 0.1f) {
			if (rig.velocity.magnitude > 0) {
				giro = velocidadGiroMovimiento * Time.deltaTime;
			} else {
				giro = velocidadGiroQuieto * Time.deltaTime;
			}
			Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, giro, 0.0F);
			Debug.DrawRay(transform.position, newDir, Color.red);
			transform.forward = newDir;
			rig.velocity = transform.forward * rig.velocity.magnitude;
		}
	}

}
