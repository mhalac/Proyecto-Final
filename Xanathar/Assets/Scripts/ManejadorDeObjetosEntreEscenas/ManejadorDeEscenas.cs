using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("Escenas Nivel De Fuego")]
	public Object[] NivelDeFuego = new Object[4];
	int ReferenciadorDeEscenas = 0;
	string NombreDeEscenas;

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
		if(col.gameObject.name == "EntradaNivelFuego" && ReferenciadorDeEscenas == 0)
		{
			NombreDeEscenas = NivelDeFuego[0].name;
			SceneManager.LoadScene(NombreDeEscenas);
			ReferenciadorDeEscenas += 1;
			print(ReferenciadorDeEscenas);
		}
		/*
		BuscadorDeEscenas = 0;
		string bru = NivelDeFuego[0].name.ToString();
		print(bru);
		SceneManager.LoadScene(bru);
		*/
	}
}
