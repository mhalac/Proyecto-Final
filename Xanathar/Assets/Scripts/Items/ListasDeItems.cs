using System.Collections;
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

		//Constructores De SubNivelesFuego
		SubNivelFuego1 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego2 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego3 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
		SubNivelFuego4 = new ConstructorEscenas(ObjetosEnEscena , PosicionesEnEscena);
	}

	public void LlenarElConstructor()
	{
		string EscenaActual = SceneManager.GetActiveScene().name;

		List <GameObject> ListaDeTodosLosObjetosEnLaEscena = new List <GameObject>(GameObject.FindGameObjectsWithTag("Items"));

		//Debug.Log("Los objetos encontrados en la escena son:" + ListaDeTodosLosObjetosEnLaEscena.Count);

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

		Debug.Log(ListaDeObjetosSeleccionados.Count + "Prestar atrencion a esto");

		ConstructorEscenas ConstructorEjemplo = new ConstructorEscenas(ListaDeObjetosSeleccionados , ListaDePosicionesEnLaEscena);

		switch (EscenaActual)
		{
			case "Lobby":
			Lobby.ReemplazarConstructorConOtro(ConstructorEjemplo);
			Debug.Log(Lobby.ConseguirListaYPos());
			break;

			case "SubNivelFuego1":
			SubNivelFuego1.ReemplazarConstructorConOtro(ConstructorEjemplo);
			Debug.Log(SubNivelFuego1.ConseguirListaYPos());
			break;

			case "SubNivelFuego2":
			SubNivelFuego2.ReemplazarConstructorConOtro(ConstructorEjemplo);
			Debug.Log(SubNivelFuego2.ConseguirListaYPos());
			break;

			case "SubNivelFuego3":
			SubNivelFuego3.ReemplazarConstructorConOtro(ConstructorEjemplo);
			Debug.Log(SubNivelFuego3.ConseguirListaYPos());
			break;

			case "SubNivelFuego4":
			SubNivelFuego4.ReemplazarConstructorConOtro(ConstructorEjemplo);
			Debug.Log(SubNivelFuego4.ConseguirListaYPos());
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
