using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "EntradaNivelFuego")
		{
			print("hola");
			SceneManager.LoadScene("SubNivel1");
		}
	}
}
