using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertasDeNiveles : MonoBehaviour {

	public GameObject Jugador;
	public ManejadorDeEscenas VariablesDeEscenas;
	public CambiadorDeNivel AsignadorDeEscenas;

	public bool EvitarCrasheo = false;

	// Use this for initialization
	void Start ()
	{
		Jugador = GameObject.FindGameObjectWithTag("Personaje");
		VariablesDeEscenas = FindObjectOfType<ManejadorDeEscenas>();
		AsignadorDeEscenas = FindObjectOfType<CambiadorDeNivel>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if(Vector3.Distance(Jugador.transform.position , this.transform.position) <= 4 && EvitarCrasheo == false)
		{
			EvitarCrasheo = true;
			string xd = VariablesDeEscenas.NivelDeFuego[0].name;
			ManejadorDeEscenas.NombreDeEscena = xd;
			AsignadorDeEscenas.IniciadorDeCambio();
			Debug.Log("Esto se cumplio");
			
		}
		*/
	}
}
