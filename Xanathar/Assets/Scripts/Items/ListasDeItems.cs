using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListasDeItems : MonoBehaviour {

	//Listas de los Objetos
	static GameObject [] ObjetosSubNivelFuego1;


	//Listas de las posiciones
	static Vector3 [] PosicionesSubNivelFuego1;

	string EscenaActual;
	public ManejadorDeEscenas GuardadorDeEscenas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		EscenaActual = SceneManager.GetActiveScene().name;
		Debug.Log(EscenaActual);
		*/
	}

	public void GuardadorDeItemsEntreEscenas()
	{
		EscenaActual = SceneManager.GetActiveScene().name;

		switch(EscenaActual)
		{

		}
	}
}
