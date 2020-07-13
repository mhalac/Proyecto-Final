using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscadorDePos : MonoBehaviour {

	GameObject PosInicial;
	GameObject PosFinal;
	GameObject Jugador;
	float PosicionX;
	float PosicionY;
	float PosicionZ;
	float RotacionY;
	// Use this for initialization
	void Start ()
	{
		PosInicial = GameObject.Find("PosicionInicial");
		PosFinal = GameObject.Find("PosicionFinal");
		Jugador = GameObject.FindGameObjectWithTag("Personaje");

		if(ManejadorDeEscenas.EntrasteOSaliste == true)
		{
			PosicionX = Mathf.RoundToInt(PosInicial.transform.position.x);
			PosicionY = Mathf.RoundToInt(PosInicial.transform.position.y);
			PosicionZ = Mathf.RoundToInt(PosInicial.transform.position.z);
		
			RotacionY = Mathf.RoundToInt(PosInicial.transform.eulerAngles.y);
		}
		else
		{
			PosicionX = Mathf.RoundToInt(PosFinal.transform.position.x);
			PosicionY = Mathf.RoundToInt(PosFinal.transform.position.y);
			PosicionZ = Mathf.RoundToInt(PosFinal.transform.position.z);
		
			RotacionY = Mathf.RoundToInt(PosFinal.transform.eulerAngles.y);
		}

		Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
		Jugador.transform.rotation = Quaternion.Euler(0f,RotacionY,0f);
		//print(PosInicial.transform.eulerAngles.y);
	}
}
