using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaJefe : MonoBehaviour {

	//Cuando se inicie el script el valor actual de vida debe ser igual al valor maximo de vida
	public float ValorDeVidaMaxima;
	public float ValorDeVidaActual;
	private float RellenoDeVida;

	public Image ContenidoBarraDeVida;

	// Use this for initialization
	void Start ()
	{
		ValorDeVidaActual = ValorDeVidaMaxima;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ManejadorDeVidaJefe();
	}

	public void ManejadorDeVidaJefe()
	{
		float VidaActual = ValorDeVidaActual;
		float VidaMaxima = ValorDeVidaMaxima;

		RellenoDeVida = VidaActual / VidaMaxima;
		ContenidoBarraDeVida.fillAmount = RellenoDeVida;

		if(VidaActual <= 0)
		{
			Debug.Log("El jefe se murio");
		}
	}
}
