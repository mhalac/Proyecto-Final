using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteEnVacios : MonoBehaviour {

	public static bool Caiste = false;

	void OnCollisionEnter(Collision collision)
	{
		EstadisticasDePersonaje VidaActual = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems FuncionRegeneracionDeVida = FindObjectOfType<ManejadorDeItems>();

		if(collision.gameObject.tag == "Personaje" && Caiste == false)
		{
			Caiste = true;
			VidaActual.VidaActualPersonaje -= 150;
			FuncionRegeneracionDeVida.ManejadorDeVida();
		}
	}
}
