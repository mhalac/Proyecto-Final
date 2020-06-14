using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoRecibido : MonoBehaviour {

	public int DañoDeEnemigos(int numero)
	{
		EstadisticasDePersonaje Stats = null;
		int Proteccion = Stats.Armadura;

		int Denominador = numero + Proteccion;
		int Numerador = numero;

		int Resultado = Numerador/Denominador;
		return Resultado;
	}
}
