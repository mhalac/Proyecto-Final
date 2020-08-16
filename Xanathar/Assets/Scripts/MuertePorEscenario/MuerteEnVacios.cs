using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteEnVacios : MonoBehaviour {

	void OnCollisionEnter(Collision collision)
	{
		EstadisticasDePersonaje VidaActual = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems FuncionRegeneracionDeVida = FindObjectOfType<ManejadorDeItems>();

		if(collision.gameObject.tag == "Personaje")
		{
			VidaActual.VidaActualPersonaje -= 150;
			FuncionRegeneracionDeVida.ManejadorDeVida();
		}
	}
}
