using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("Escenas Nivel De Fuego")]
	public Object[] NivelDeFuego = new Object[5];
	int ReferenciadorDeEscenas = 0;
	string NombreDeEscenaProxima;
	string NombreDeEscenaActual;
	bool[] ArrayDeNiveles = new bool[]{ false, false, false, false };
	bool ActivadorDeCambio = false;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnCollisionEnter(Collision col)
	{
		/*
		if(col.gameObject.name == "EntradaNivelFuego" && ReferenciadorDeEscenas == 0)
		{
			ArrayDeNiveles[0] = true;
			ReferenciadorDeEscenas += 1;
			NombreDeEscenas = NivelDeFuego[ReferenciadorDeEscenas].name;
			SceneManager.LoadScene(NombreDeEscenas);
		}

		if(col.gameObject.name == "Entrada")
		{
			
		}

		if(col.gameObject.name == "Salida" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscenas = NivelDeFuego[ReferenciadorDeEscenas].name;
			SceneManager.LoadScene(NombreDeEscenas);
			print(NombreDeEscenas);
		}
		
		BuscadorDeEscenas = 0;
		string bru = NivelDeFuego[0].name.ToString();
		print(bru);
		SceneManager.LoadScene(bru);
		*/

		/*
		if(col.gameObject.name == "EntradaNivelFuego" && ArrayDeNiveles[0] == false)
		{
			ArrayDeNiveles[0] = true;
			NombreDeEscenaActual = NivelDeFuego[ReferenciadorDeEscenas].name;
			ReferenciadorDeEscenas += 1;
			NombreDeEscenaProxima = NivelDeFuego[ReferenciadorDeEscenas].name;
			SceneManager.LoadScene(NombreDeEscenaProxima);
		}

		if(col.gameObject.name == "Salida" && NombreDeEscenaActual != NombreDeEscenaProxima)
		{
			
		}
		*/
		if(col.gameObject.name == "EntradaNivelFuego")
		{
			print("Hola");
		}
	}

	private void Gestionador()
	{
		
	}
}
