using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManejadorHUD : MonoBehaviour {

	public GameObject Pasivas;
	public GameObject Estadisticas;
	int Contador = 0;
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.I) && Contador == 0)
		{
			Pasivas.SetActive(false);
			Estadisticas.SetActive(false);
			Contador +=1;
		}
		else if(Input.GetKeyDown(KeyCode.I) && Contador == 1)
		{
			Pasivas.SetActive(true);
			Estadisticas.SetActive(true);
			Contador = 0;
		}
	}
}
