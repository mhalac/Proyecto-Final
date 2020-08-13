using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDeEstadistica : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public static void ManejadorDeEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		//Debug.Log(Estadisticas);
		
		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			break;
		}
		
	}
}
