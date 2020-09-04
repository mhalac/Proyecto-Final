using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoRecibido : MonoBehaviour {

	public void DañoDeEnemigos(float numero)
	{
		EstadisticasDePersonaje Estadisticas = EstadisticasDePersonaje.FindObjectOfType<EstadisticasDePersonaje>();

		int Proteccion = Estadisticas.Armadura;

		float DañoMasArmadura = numero + Proteccion;
		float Daño = numero;
		
		float Resultado = Daño/DañoMasArmadura;
		Estadisticas.VidaActualPersonaje -= Resultado;
		print(Estadisticas.VidaActualPersonaje);
	}
}
