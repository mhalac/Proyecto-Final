﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionIconos : MonoBehaviour {

	[Header("Imagenes De Las Habilidades")]
	public Image ActivaDeFuego;

	[Header("Cooldown")]
	public float CoolDown;

	[Header("Bools De Las Habilidades")]
	public bool ActivaDeFuegoCooldown = false;

	// Update is called once per frame
	void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.N))
		{
			ActivaDeFuegoCooldown = true;
			SeleccionadorDeImagenes(5);
		}
		
	}

	IEnumerator CoolDownDeHabilidades(Image ImagenDeActiva , float CoolDownHabilidad)
	{

		Debug.Log("Corrutina Iniciada");
		ImagenDeActiva.fillAmount = 0;
		while (ImagenDeActiva.fillAmount <= 1)
		{
			yield return new WaitForEndOfFrame();
			ImagenDeActiva.fillAmount += (1 / CoolDownHabilidad * Time.deltaTime);
			//Debug.Log(ImagenDeActiva.fillAmount);

			if(ImagenDeActiva.fillAmount >= 1)
			{
				ImagenDeActiva.fillAmount = 1f;
				Debug.Log("Animacion Completada");
				yield break;
			}
		}
	}

	public void SeleccionadorDeImagenes(float CooldownP)
	{
		CoolDown = CooldownP;
		if(ActivaDeFuegoCooldown)
		{
			
			StartCoroutine(CoolDownDeHabilidades(ActivaDeFuego , CoolDown));
			return;
		}
		//Debug.Log("Este no se deberia de ejecutar");
	}

	public void ResetIcono()
	{
		ActivaDeFuego.fillAmount = 1f;
	}
}
