﻿using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaJefe : MonoBehaviour {

	//Cuando se inicie el script el valor actual de vida debe ser igual al valor maximo de vida
	public float ValorDeVidaMaxima;
	//a ValorDeVidaActual metele la vida del jefe
	public float ValorDeVidaActual;
	private float RellenoDeVida;
	public Image ContenidoBarraDeVida;

	// Use this for initialization
	void Start ()
	{
		//Igualas la vida maxzima a la vida actual
		ValorDeVidaMaxima = ValorDeVidaActual;
	}
	
	// Update is called once per frame
	void Update ()
	{
		ManejadorDeVidaJefe(ValorDeVidaMaxima);
	}

	public void ManejadorDeVidaJefe(float VidaMaximaDelJefe)
	{
		RellenoDeVida = ValorDeVidaActual / VidaMaximaDelJefe;
		ContenidoBarraDeVida.fillAmount = RellenoDeVida;

		if(ValorDeVidaActual <= 0)
		{
			Debug.Log("El jefe se murio");
		}
	}
}
