using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasDePersonaje : MonoBehaviour {

	//Estadisticas Basicas
	public int VidaDePersonaje = 4;
	public float DañoDePersonajeNormal = 10;
	public float TiempoCooldown = 10f;
	public int Armadura = 4;
	public float Gravedad = -15f;


	public float VelocidadDeMovimiento = 10f;
	public float VelocidadDeAtaque = 1f;
	public float FuerzaDeSalto = 3f;


	//Daños Elementales
	public float DañoElementalFuego = 0;
	public float DañoElementalAire = 0;
	public float DañoElementalAgua = 0;
	public float DañoElementalTierra = 0;
}
