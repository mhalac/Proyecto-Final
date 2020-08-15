using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertePorCaida : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Personaje")
		{
			EstadisticasDePersonaje Vida = FindObjectOfType<EstadisticasDePersonaje>();
			ManejadorDeItems FuncionDeVida = FindObjectOfType<ManejadorDeItems>();

			Vida.VidaActualPersonaje -= 100;
			FuncionDeVida.ManejadorDeVida();
		}
	}
}
