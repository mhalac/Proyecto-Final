using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantenedorDeGameObjects : MonoBehaviour {

	static GameObject CanvasHUD;
	static GameObject JugadorPrincipal;


	// Use this for initialization
	void Start ()
	{
		if(CanvasHUD != null && JugadorPrincipal != null)
		{
			Destroy(CanvasHUD);
			Destroy(JugadorPrincipal);
		}
		CanvasHUD = GameObject.FindGameObjectWithTag("HUDPrincipal");
		JugadorPrincipal = GameObject.FindGameObjectWithTag("Personaje");

		DontDestroyOnLoad(CanvasHUD);
		DontDestroyOnLoad(JugadorPrincipal);
	}
}
