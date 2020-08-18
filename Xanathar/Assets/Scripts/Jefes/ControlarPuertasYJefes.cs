using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlarPuertasYJefes : MonoBehaviour {

	public static bool JefeDeFuegoMuerto = false;

	public GameObject [] Puertas = new GameObject [2];

	public static bool JefeEliminado = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		if(Input.GetKeyDown(KeyCode.O))
		{
			OcultarTodasLasPuertas();
		}

		if(Input.GetKeyDown(KeyCode.U))
		{
			ReaparecerPuertas();
		}
		*/
	}

	public void OcultarTodasLasPuertas()
	{
		GameObject [] TodasLasPuertas = GameObject.FindGameObjectsWithTag("PuertasDeJefe");

		for(int i = 0; i < TodasLasPuertas.Length; i++)
		{
			TodasLasPuertas[i].SetActive(false);
		}
	}

	public void ReaparecerPuertas()
	{
		for(int i = 0; i < Puertas.Length; i++)
		{
			Puertas[i].SetActive(true);
		}
	}
}
