﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ListasDeItems : MonoBehaviour {

	public static List <GameObject> ObjetosEnEscena;
	public static List <Vector3> PosicionesEnEscena;

	//Constructores De Los Niveles
	ConstructorEscenas Lobby;
	ConstructorEscenas SubNivelFuego1;
	ConstructorEscenas SubNivelFuego2;
	ConstructorEscenas SubNivelFuego3;
	ConstructorEscenas SubNivelFuego4;
	ConstructorEscenas SubNivelTierra1;
	ConstructorEscenas SubNivelTierra2;
	ConstructorEscenas SubNivelTierra3;
	ConstructorEscenas SubNivelTierra4;
	ConstructorEscenas SubNivelViento1;
	ConstructorEscenas SubNivelViento2;
	ConstructorEscenas SubNivelViento3;
	ConstructorEscenas SubNivelViento4;
	ConstructorEscenas SubNivelAgua1;
	ConstructorEscenas SubNivelAgua2;
	ConstructorEscenas SubNivelAgua3;
	ConstructorEscenas SubNivelAgua4;
	ConstructorEscenas NivelFinal;

	public List <GameObject> ListaDePrefabs = new List<GameObject>();


	// Use this for initialization
	void Start ()
	{
		ObjetosEnEscena = new List<GameObject>(1);
		PosicionesEnEscena = new List<Vector3>(1);

		foreach (GameObject Prefab in Resources.LoadAll("Objetos" , typeof (GameObject)))
		{
			ListaDePrefabs.Add(Prefab);
		}

		ConstruirConsatructores();
	}

	/*
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.I))
		{
			LlenarElConstructor();
		}

		if(Input.GetKeyDown(KeyCode.P))
		{
			InstanciarLosObjetosDelConstructor();
		}
	}
	*/

	private void ConstruirConsatructores()
	{
		//Constructor Del TestMenda
		Lobby = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);

		//Constructores del nivel de fuego
		SubNivelFuego1 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego2 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego3 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego4 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);

		//Constructores del nivel de tierra
		SubNivelTierra1 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelTierra2 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelTierra3 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelTierra4 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);

		//Constructores del nivel de viento
		SubNivelViento1 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelViento2 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelViento3 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelViento4 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);

		//Constructores del nivel de agua
		SubNivelAgua1 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelAgua2 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelAgua3 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelAgua4 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);

		//Constructor NivelFinal
		NivelFinal = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
	}

	public void LlenarElConstructor()
	{
		string EscenaActual = SceneManager.GetActiveScene().name;

		List <GameObject> ListaDeTodosLosObjetosEnLaEscena = new List <GameObject>(GameObject.FindGameObjectsWithTag("Items"));

		Debug.Log("Los objetos encontrados en la escena son:" + ListaDeTodosLosObjetosEnLaEscena.Count);

		for(int i = 0; i < ListaDeTodosLosObjetosEnLaEscena.Count; i++)
		{
			Debug.Log(ListaDeTodosLosObjetosEnLaEscena[i].name);
		}

		List <Vector3> ListaDePosicionesEnLaEscena = new List<Vector3>();

		List <GameObject> ListaDeObjetosSeleccionados = new List<GameObject>();

		for(int i = 0; i < ListaDeTodosLosObjetosEnLaEscena.Count; i++)
		{
			for(int o = 0; o < ListaDePrefabs.Count; o++)
			{
				if(ListaDeTodosLosObjetosEnLaEscena[i].GetComponent<InformacionDeItems>().Nombre == ListaDePrefabs[o].GetComponent<InformacionDeItems>().Nombre)
				{
					ListaDeObjetosSeleccionados.Add(ListaDePrefabs[o]);
					ListaDePosicionesEnLaEscena.Add(ListaDeTodosLosObjetosEnLaEscena[i].transform.position);

					break;
				}
			}
		}

		//Debug.Log(ListaDeObjetosSeleccionados.Count + "Prestar atrencion a esto");

		ConstructorEscenas ConstructorEjemplo = new ConstructorEscenas(ListaDeObjetosSeleccionados , ListaDePosicionesEnLaEscena);

		switch (EscenaActual)
		{
			case "Lobby":
			Lobby.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(Lobby.ConseguirListaYPos());
			break;

			case "SubNivelFuego1":
			SubNivelFuego1.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelFuego1.ConseguirListaYPos());
			break;

			case "SubNivelFuego2":
			SubNivelFuego2.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelFuego2.ConseguirListaYPos());
			break;

			case "SubNivelFuego3":
			SubNivelFuego3.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelFuego3.ConseguirListaYPos());
			break;

			case "SubNivelFuego4":
			SubNivelFuego4.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelFuego4.ConseguirListaYPos());
			break;

			case "SubNivelTierra1":
			SubNivelTierra1.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelTierra1.ConseguirListaYPos());
			break;

			case "SubNivelTierra2":
			SubNivelTierra2.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelTierra2.ConseguirListaYPos());
			break;

			case "SubNivelTierra3":
			SubNivelTierra3.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelTierra3.ConseguirListaYPos());
			break;

			case "SubNivelTierra4":
			SubNivelTierra4.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelTierra4.ConseguirListaYPos());
			break;

			case "SubNivelViento1":
			SubNivelViento1.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelViento1.ConseguirListaYPos());
			break;
			
			case "SubNivelViento2":
			SubNivelViento2.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelViento2.ConseguirListaYPos());
			break;

			case "SubNivelViento3":
			SubNivelViento3.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelViento3.ConseguirListaYPos());
			break;

			case "SubNivelViento4":
			SubNivelViento4.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelViento4.ConseguirListaYPos());
			break;

			case "SubNivelAgua1":
			SubNivelAgua1.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelAgua1.ConseguirListaYPos());
			break;

			case "SubNivelAgua2":
			SubNivelAgua2.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelAgua2.ConseguirListaYPos());
			break;

			case "SubNivelAgua3":
			SubNivelAgua3.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelAgua3.ConseguirListaYPos());
			break;

			case "SubNivelAgua4":
			SubNivelAgua4.ReemplazarConstructorConOtro(ConstructorEjemplo);
			//Debug.Log(SubNivelAgua4.ConseguirListaYPos());
			break;

			case "LvlFinal":
			NivelFinal.ReemplazarConstructorConOtro(ConstructorEjemplo);
			break;

			default:
			Debug.Log("ERROR ACA NO HAY NADA");
			break;
		}
		
	}

	public void InstanciarLosObjetosDelConstructor()
	{
		string EscenaActual = SceneManager.GetActiveScene().name;

		List <GameObject> ObjetosAInstanciar = new List<GameObject>();
		List <Vector3> PosicionesAInstanciar = new List<Vector3>();

		switch (EscenaActual)
		{
			case "Lobby":
			Lobby.ConseguirListaDeObjetos(ObjetosAInstanciar);
			Lobby.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelFuego1":
			SubNivelFuego1.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelFuego1.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelFuego2":
			SubNivelFuego2.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelFuego2.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelFuego3":
			SubNivelFuego3.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelFuego3.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelFuego4":
			SubNivelFuego4.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelFuego4.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelTierra1":
			SubNivelTierra1.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelTierra1.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelTierra2":
			SubNivelTierra2.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelTierra2.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelTierra3":
			SubNivelTierra3.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelTierra3.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelTierra4":
			SubNivelTierra4.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelTierra4.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelViento1":
			SubNivelViento1.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelViento1.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelViento2":
			SubNivelViento2.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelViento2.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelViento3":
			SubNivelViento3.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelViento3.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelViento4":
			SubNivelViento4.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelViento4.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelAgua1":
			SubNivelAgua1.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelAgua1.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelAgua2":
			SubNivelAgua2.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelAgua2.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelAgua3":
			SubNivelAgua3.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelAgua3.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "SubNivelAgua4":
			SubNivelAgua4.ConseguirListaDeObjetos(ObjetosAInstanciar);
			SubNivelAgua4.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			case "LvlFinal":
			NivelFinal.ConseguirListaDeObjetos(ObjetosAInstanciar);
			NivelFinal.ConseguirListaDePosiciones(PosicionesAInstanciar);
			break;

			default:
			Debug.Log("ERROR ACA NO HAY NADA");
			break;
		}

		int Index = ObjetosAInstanciar.Count;

		for(int i = 0; i < Index; i++)
		{	
			Instantiate(ObjetosAInstanciar[i] , PosicionesAInstanciar[i] , Quaternion.identity);
		}
	}
}
