using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiparEstadisticasYPasivas : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void EquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		
		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			Estadisticas.DañoDePersonajeNormal += 5;
			Estadisticas.Armadura += 5;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal += 5;
			//Falta La regeneracion de vida
			break;
		}
	}

	public static void DesEquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();

		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			Estadisticas.DañoDePersonajeNormal -= 5;
			Estadisticas.Armadura -= 5;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal -= 5;
			//Falta La regeneracion de vida
			break;
		}
	}
}
