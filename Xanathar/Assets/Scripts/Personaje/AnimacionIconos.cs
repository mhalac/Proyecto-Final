using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimacionIconos : MonoBehaviour {

	[Header("Imagenes De Las Habilidades")]
	public Image ActivaDeFuego;
	public Image ActivaDeTierra;
	public Image ActivaDeViento;
	public Image ActivaDeAgua;

	[Header("Bools De Las Habilidades")]
	public bool ActivaDeFuegoCooldown = false;
	public bool ActivaDeTierraCooldown = false;
	public bool ActivaDeVientoCooldown = false;
	public bool ActivaDeAguaCooldown = false;

	void Start()
	{
		
		//ActivaDeTierraCooldown = true;
		//ActivaDeFuegoCooldown = true;
		//ActivaDeVientoCooldown = true;
		//ActivaDeAguaCooldown = true;
		
	}
	// Update is called once per frame
	void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.N))
		{
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
		if(ActivaDeFuegoCooldown)
		{
			StartCoroutine(CoolDownDeHabilidades(ActivaDeFuego , CooldownP));
			ActivaDeFuegoCooldown = false;
			return;
		}

		if(ActivaDeTierraCooldown)
		{
			StartCoroutine(CoolDownDeHabilidades(ActivaDeTierra , CooldownP));
			ActivaDeTierraCooldown = false;
			return;
		}

		if(ActivaDeVientoCooldown)
		{
			StartCoroutine(CoolDownDeHabilidades(ActivaDeViento , CooldownP));
			ActivaDeVientoCooldown = false;
			return;
		}

		if(ActivaDeAguaCooldown)
		{
			StartCoroutine(CoolDownDeHabilidades(ActivaDeAgua , CooldownP));
			ActivaDeAguaCooldown = false;
			return;
		}
		//Debug.Log("Este no se deberia de ejecutar");
	}
}
