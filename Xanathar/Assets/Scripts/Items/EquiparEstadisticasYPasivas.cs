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
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();

		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			Estadisticas.DañoDePersonajeNormal += 2;
			Estadisticas.Armadura += 5;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal += 2;
			Estadisticas.TiempoDeRegeneracion -= 2;
			break;

			default:
			Debug.Log("Nombre No Encontrado");
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}

	public static void DesEquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();

		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			Estadisticas.DañoDePersonajeNormal -= 2;
			Estadisticas.Armadura -= 5;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal -= 2;
			Estadisticas.TiempoDeRegeneracion += 2;
			break;

			default:
			Debug.Log("Nombre no encontrado");
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}

	public static void EquiparPasiva(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();

		switch(NombreDelItem)
		{
			case "Fire Chestplate":
			Estadisticas.DañoDePersonajeNormal -= Estadisticas.DañoDePersonajeInicial / 2;
			Estadisticas.Armadura += Estadisticas.ArmaduraInicial / 2;
			break;

			case "Old Flame":
			Estadisticas.Armadura -= Estadisticas.ArmaduraInicial / 2;
			Estadisticas.DañoDePersonajeNormal += Estadisticas.DañoDePersonajeInicial * 2;
			break;
		}
		
		manejadorDeItems.ManejadorDeVida();
	}

	public static void DesEquiparPasiva(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();
		
		switch(NombreDelItem)
		{
			case "Fire Chestplate":
			Estadisticas.DañoDePersonajeNormal += Estadisticas.DañoDePersonajeInicial / 2;
			Estadisticas.Armadura -= Estadisticas.ArmaduraInicial / 2;
			break;

			case "Old Flame":
			Estadisticas.Armadura += Estadisticas.ArmaduraInicial / 2;
			Estadisticas.DañoDePersonajeNormal -= Estadisticas.DañoDePersonajeInicial * 2;
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}
}
