using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquiparEstadisticasYPasivas : MonoBehaviour {

	public GameObject PrefabMisil;

	public void EquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();
		GestorItems Gestor = FindObjectOfType<GestorItems>();

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
			Gestor.AplicarCDR(20);
			break;

			case "Estadistica De Viento 1":
			Estadisticas.VelocidadDeMovimiento += 2;
			Gestor.AplicarCDR(20);
			break;

			case "Estadistica de Viento 2":
			Estadisticas.VelocidadDeMovimiento += 2;
			Estadisticas.DañoDePersonajeNormal += 2;
			break;

			default:
			Debug.Log("Nombre No Encontrado");
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}

	public void DesEquiparEstadistica(string NombreDelItem)
	{
		EstadisticasDePersonaje Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems manejadorDeItems = FindObjectOfType<ManejadorDeItems>();
		GestorItems Gestor = FindObjectOfType<GestorItems>();

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
			Gestor.AplicarCDR(0);
			break;

			case "Estadistica De Viento 1":
			Estadisticas.VelocidadDeMovimiento += 2;
			Gestor.AplicarCDR(0);
			break;

			case "Estadistica de Viento 2":
			Estadisticas.VelocidadDeMovimiento -= 2;
			Estadisticas.DañoDePersonajeNormal -= 2;
			break;

			default:
			Debug.Log("Nombre no encontrado");
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}

	public void EquiparPasiva(string NombreDelItem)
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

			case "Windigo":
			Estadisticas.LanzarMisiles = true;
			StartCoroutine("EsperarParaMisiles");
			break;

			case "Whirlwind":
			Estadisticas.VelocidadDeDash += 3;
			break;
		}
		
		manejadorDeItems.ManejadorDeVida();
	}

	public void DesEquiparPasiva(string NombreDelItem)
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
			Estadisticas.ContadorRoboDeVida = 0;
			break;

			case "Windigo":
			Estadisticas.LanzarMisiles = false;
			StopCoroutine("EsperarParaMisiles");
			break;

			case "Whirlwind":
			Estadisticas.VelocidadDeDash -= 3;
			break;
		}

		manejadorDeItems.ManejadorDeVida();
	}

	public IEnumerator EsperarParaMisiles()
	{
		yield return new WaitForSeconds(10f);

		if(FindObjectOfType<EstadisticasDePersonaje>().LanzarMisiles == true)
		{
			CastearMisil();
			yield return null;
		}
		else
		{
			yield return null;
		}
	}

	public void CastearMisil()
	{
		GameObject Personaje = GameObject.FindGameObjectWithTag("Personaje");
		
		var Bala = Instantiate(PrefabMisil , Personaje.transform.position , Quaternion.identity);
		Destroy(Bala , 5);

		StartCoroutine(EsperarParaMisiles());
	}
}
