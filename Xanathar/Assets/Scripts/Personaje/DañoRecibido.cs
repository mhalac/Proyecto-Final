using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoRecibido : MonoBehaviour {

	public void DañoDeEnemigos(float numero)
	{
		int Proteccion = EstadisticasDePersonaje.Armadura;

		float DañoMasArmadura = numero + Proteccion;
		float Daño = numero;

		float Resultado = Daño/DañoMasArmadura;
		EstadisticasDePersonaje.VidaDePersonaje -= Resultado;
		print(EstadisticasDePersonaje.VidaDePersonaje);
	}
}
