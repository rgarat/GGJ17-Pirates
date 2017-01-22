using UnityEngine;
using System.Collections;
using System.IO;

public enum TipoCaptura{
	simple, secuencia, ninguna
}

public enum Formato{
	png, jpeg
}

public enum Directorio{
	proyecto, assets, personalizado
}

public class ScreenshotScript : MonoBehaviour {

	public TipoCaptura tipoCaptura;
	public Formato formatoImagen;
	public KeyCode tecla = KeyCode.Space;
	private bool secuencia = false;
	[Space]
	public string NombreScreenshot = "Screenshot";

	private int numeroS = 0;
	private string numeroScreenshot;
	private bool primerScreenshot = true;
	private int totalCapturasSecuencia = 0;
	private string direccion;
	private int numeroC = 0;
	private string numeroCarpeta;
	private int numeroCS = 0;
	private string numeroCarpetaSecuencia;
	[Space]
	public Directorio directorio;
	public string directorioPersonalizado = "C:/Users/userName/Desktop";


	void Start(){
		
		switch (tipoCaptura) {

		case TipoCaptura.simple:
			
			print ("Presiona (" + tecla.ToString () + ") para tomar un Screenshot");

			break;

		case TipoCaptura.secuencia:
			
			print ("Presiona (" + tecla.ToString () + ") para comenzar la secuencia, presiona nuevamente (" + tecla.ToString () + ") para detener la captura de secuencia");
			NombreScreenshot = NombreScreenshot+"_Secuencia"; 
			break;
		}

	}

	void Update(){
		
		if (Input.GetKeyDown (tecla)) {
			switch (tipoCaptura) {

			case TipoCaptura.simple:
				if (primerScreenshot == true) {
					NumeracionCarpeta ();
					primerScreenshot = false;
				}
				ScreenShot ();
				print ("Screenshot: " + NombreScreenshot + "_" + numeroScreenshot + "." + formatoImagen.ToString ());
				break;

			case TipoCaptura.secuencia:
				if (secuencia == false) {
					NumeracionCarpeta ();
					secuencia = true;
					print ("Comienza captura en secuencia");
				} else {
					secuencia = false;
					print ("Finaliza captura en secuencia: "+totalCapturasSecuencia.ToString()+" capturas.");
					numeroS = 0;
					totalCapturasSecuencia = 0;
				}
				break;
			}
		}

		if (secuencia == true) {
			ScreenShot ();
			totalCapturasSecuencia++;
		}
	}

	void ScreenShot(){
		bool sacaScreenshot = true;
		if (numeroS < 10) {
			numeroScreenshot = "000" + numeroS.ToString ();
		} else if (numeroS >= 10 && numeroS < 100) {
			numeroScreenshot = "00" + numeroS.ToString ();
		} else if (numeroS >= 100 && numeroS < 1000) {
			numeroScreenshot = "0" + numeroS.ToString ();
		} else if (numeroS >= 1000 && numeroS <= 9999) {
			numeroScreenshot = numeroS.ToString ();
		} else {
			sacaScreenshot = false;
			numeroS = 0;
			NumeracionCarpeta ();
			ScreenShot ();
		}

		if (sacaScreenshot == true) {
			Application.CaptureScreenshot (direccion + NombreScreenshot + "_" + numeroScreenshot + "." + formatoImagen.ToString ());
			numeroS++;
		}
	}

	void NumeracionCarpeta(){

		if (tipoCaptura == TipoCaptura.simple) {
			if (numeroC < 10) {
				numeroCarpeta = "000" + numeroC.ToString ();
			} else if (numeroC >= 10 && numeroC < 100) {
				numeroCarpeta = "00" + numeroC.ToString ();
			} else if (numeroC >= 100 && numeroC < 1000) {
				numeroCarpeta = "0" + numeroC.ToString ();
			} else if (numeroC >= 1000) {
				numeroCarpeta = numeroC.ToString ();
			}

			if (directorio == Directorio.proyecto) {

				direccion = (Directory.GetCurrentDirectory ()).ToString ();

				if (!Directory.Exists (direccion + "/Screenshots_" + numeroCarpeta + "/")) {
			
					Directory.CreateDirectory (direccion + "/Screenshots_" + numeroCarpeta + "/");

					direccion = direccion + "/Screenshots_" + numeroCarpeta + "/";

				} else {
					numeroC++;
					NumeracionCarpeta ();
				}
			}
			if (directorio == Directorio.assets) {

				direccion = (Directory.GetCurrentDirectory ()).ToString ();

				if (!Directory.Exists (direccion+"/Assets/"+ "/Screenshots_" + numeroCarpeta + "/")) {

					Directory.CreateDirectory (direccion+"/Assets/" + "/Screenshots_" + numeroCarpeta + "/");

					direccion = direccion+"/Assets/"+ "/Screenshots_" + numeroCarpeta + "/";

				} else {
					numeroC++;
					NumeracionCarpeta ();
				}
			}
			if (directorio == Directorio.personalizado) {

				direccion = directorioPersonalizado;

				if (!Directory.Exists (direccion + "/Screenshots_" + numeroCarpeta + "/")) {

					Directory.CreateDirectory (direccion + "/Screenshots_" + numeroCarpeta + "/");

					direccion = direccion + "/Screenshots_" + numeroCarpeta + "/";

				} else {
					numeroC++;
					NumeracionCarpeta ();
				}
			}
		}



		if (tipoCaptura == TipoCaptura.secuencia) {
			
			if (numeroCS < 10) {
				numeroCarpetaSecuencia = "000" + numeroCS.ToString ();
			} else if (numeroCS >= 10 && numeroCS < 100) {
				numeroCarpetaSecuencia = "00" + numeroCS.ToString ();
			}else if (numeroCS >= 100 && numeroCS < 1000) {
				numeroCarpetaSecuencia = "0" + numeroCS.ToString ();
			}else if (numeroCS >= 1000){
				numeroCarpetaSecuencia = numeroCS.ToString ();
			}

			if (directorio == Directorio.proyecto) {
				direccion = (Directory.GetCurrentDirectory ()).ToString ();

				if (!Directory.Exists (direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/")) {

					Directory.CreateDirectory (direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/");

					direccion = direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/";

				} else {
					numeroCS++;
					NumeracionCarpeta ();
				}
			}

			if (directorio == Directorio.assets) {
				direccion = (Directory.GetCurrentDirectory ()).ToString ();

				if (!Directory.Exists (direccion+"/Assets/" + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/")) {

					Directory.CreateDirectory (direccion+"/Assets/" + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/");

					direccion = direccion+"/Assets/" + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/";

				} else {
					numeroCS++;
					NumeracionCarpeta ();
				}
			}

			if (directorio == Directorio.personalizado) {
				
				direccion = directorioPersonalizado;

				if (!Directory.Exists (direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/")) {

					Directory.CreateDirectory (direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/");

					direccion = direccion + "/Screenshots Secuencia_" + numeroCarpetaSecuencia + "/";

				} else {
					numeroCS++;
					NumeracionCarpeta ();
				}
			}


		}

	}

}
