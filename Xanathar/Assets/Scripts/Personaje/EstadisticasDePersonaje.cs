using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasDePersonaje : MonoBehaviour {

	//Estadisticas Basicas
	public int VidaDePersonaje = 4;
	public float DañoDePersonajeNormal = 10f;
	public float TiempoCooldown = 10f;
	public int Armadura = 4;
	public float Gravedad = -15f;


	public float VelocidadDeMovimiento = 10f;
	public float VelocidadDeAtaque = 1f;
	public float FuerzaDeSalto = 3f;


	//Daños Elementales
	public float DañoElementalFuego = 0f;
	public float DañoElementalAire = 0f;
	public float DañoElementalAgua = 0f;
	public float DañoElementalTierra = 0f;

	void Start()
	{
		PlayerPrefs.SetInt("Vida", VidaDePersonaje);
		PlayerPrefs.SetFloat("DañoNormal", DañoDePersonajeNormal);
		PlayerPrefs.SetFloat("Cooldown", TiempoCooldown);
		PlayerPrefs.SetInt("Armadura", Armadura);
		PlayerPrefs.SetFloat("Gravedad", Gravedad);

		PlayerPrefs.SetFloat("VelocidadDeMovimiento", VelocidadDeMovimiento);
		PlayerPrefs.SetFloat("VelocidadDeAtaque", VelocidadDeAtaque);
		PlayerPrefs.SetFloat("FuerzaDeSalto", FuerzaDeSalto);

		PlayerPrefs.SetFloat("DañoFuego", DañoElementalFuego);
		PlayerPrefs.SetFloat("DañoViento", DañoElementalAire);
		PlayerPrefs.SetFloat("DañoAgua", DañoElementalAgua);
		PlayerPrefs.SetFloat("DañoTierra", DañoElementalTierra);
	}
}
