﻿using System.Collections;
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
			Estadisticas.TiempoDeRegeneracion -= 2;
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
			Estadisticas.TiempoDeRegeneracion += 2;
			break;

			default:
			Debug.Log("Nombre no encontrado");
			break;
		}
	}

	public static void EquiparPasiva(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();

		switch(NombreDelItem)
		{
			case "Fire Chestplate":
			Estadisticas.DañoDePersonajeNormal = Estadisticas.DañoDePersonajeInicial / 2;
			Estadisticas.Armadura = Estadisticas.ArmaduraInicial * 2;
			break;

			case "Old Flame":
			Estadisticas.VidaMaximaPersonaje = Estadisticas.VidaMaximaDePersonajeInicial / 2;
			Estadisticas.DañoDePersonajeNormal = Estadisticas.DañoDePersonajeInicial * 2;
			break;
		}
	}

	public static void DesEquiparPasiva(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		
		switch(NombreDelItem)
		{
			case "Fire Chestplate":
			Estadisticas.DañoDePersonajeNormal = Estadisticas.DañoDePersonajeInicial * 2;
			Estadisticas.Armadura = Estadisticas.ArmaduraInicial / 2;
			break;

			case "Old Flame":
			Estadisticas.VidaMaximaPersonaje = Estadisticas.VidaMaximaDePersonajeInicial * 2;
			Estadisticas.DañoDePersonajeNormal = Estadisticas.DañoDePersonajeInicial / 2;
			break;
		}
	}
}