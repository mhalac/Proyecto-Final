using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscadorDePos : MonoBehaviour {

	GameObject PosInicial;
	GameObject PosFinal;
	GameObject Jugador;
	int PosicionX;
	int PosicionY;
	int PosicionZ;
	// Use this for initialization
	void Start ()
	{
		PosInicial = GameObject.Find("PosicionInicial");
		Jugador = GameObject.FindGameObjectWithTag("Personaje");
		
		PosicionX = Mathf.RoundToInt(PosInicial.transform.position.x);
		PosicionY = Mathf.RoundToInt(PosInicial.transform.position.y);
		PosicionZ = Mathf.RoundToInt(PosInicial.transform.position.z);
		Jugador.transform.position = new Vector3(PosicionX,PosicionY,PosicionZ);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
