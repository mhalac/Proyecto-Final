using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiparEstadisticasYPasivas : MonoBehaviour {

	public static void EquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();

		switch(NombreDelItem)
		{
			case "Estadistica De Fuego 1":
			Estadisticas.DañoDePersonajeNormal += 2;
			Estadisticas.Armadura += 2;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal += 2;
			Estadisticas.TiempoDeRegeneracion -= 2;
			break;

			case "Estadistica De Tierra 1":
			Estadisticas.Armadura += 2;
			Estadisticas.TiempoDeRegeneracion -= 2;
			break;

			case "Estadistica De Tierra 2":
			Estadisticas.Armadura += 2;
			GestorItems Gestor = FindObjectOfType<GestorItems>();
			Gestor.AplicarCDR(20);
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
			Estadisticas.Armadura -= 2;
			break;

			case "Estadistica De Fuego 2":
			Estadisticas.DañoDePersonajeNormal -= 2;
			Estadisticas.TiempoDeRegeneracion += 2;
			break;

			case "Estadistica De Tierra 1":
			Estadisticas.Armadura -= 2;
			Estadisticas.TiempoDeRegeneracion += 2;
			break;

			case "Estadistica De Tierra 2":
			Estadisticas.Armadura -= 2;
			GestorItems Gestor = FindObjectOfType<GestorItems>();
			Gestor.AplicarCDR(0);
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
			Estadisticas.DañoDePersonajeNormal -= Estadisticas.DañoDePersonajeInicialCopia/ 2;
			Estadisticas.Armadura += Estadisticas.ArmaduraInicialCopia / 2;
			break;

			case "Old Flame":
			Estadisticas.Armadura -= Estadisticas.ArmaduraInicialCopia / 2;
			Estadisticas.DañoDePersonajeNormal += Estadisticas.DañoDePersonajeInicialCopia * 2;
			break;

			case "Turtle Shell":
			Estadisticas.Armadura += Estadisticas.ArmaduraInicialCopia / 3;
			Estadisticas.VelocidadDeMovimiento -= 3;
			break;

			case "Estalactita":
			Estadisticas.RoboDeVida = true;
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
			Estadisticas.DañoDePersonajeNormal += Estadisticas.DañoDePersonajeInicialCopia / 2;
			Estadisticas.Armadura -= Estadisticas.ArmaduraInicialCopia / 2;
			break;

			case "Old Flame":
			Estadisticas.Armadura += Estadisticas.ArmaduraInicialCopia / 2;
			Estadisticas.DañoDePersonajeNormal -= Estadisticas.DañoDePersonajeInicialCopia * 2;
			break;

			case "Turtle Shell":
			Estadisticas.Armadura -= Estadisticas.ArmaduraInicialCopia / 2;
			Estadisticas.VelocidadDeMovimiento += 3;
			break;

			case "Estalactita":
			Estadisticas.RoboDeVida = false;
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}
}
