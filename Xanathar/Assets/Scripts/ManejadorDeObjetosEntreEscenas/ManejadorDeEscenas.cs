using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("Escenas Nivel De Fuego")]
	public Object[] NivelDeFuego = new Object[5];
	int ReferenciadorDeEscenas = 0;
	public static string NombreDeEscena;
	public static bool EntrasteOSaliste = false;
	public static bool ActivadorDeCambio = false;
	EstadisticasDePersonaje VelCorrer;
	CambiadorDeNivel AsignadorDeEscenas;

	// Use this for initialization
	void Start ()
	{
		VelCorrer = FindObjectOfType<EstadisticasDePersonaje>();
		AsignadorDeEscenas = FindObjectOfType<CambiadorDeNivel>();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "EntradaNivelFuego" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscena = NivelDeFuego[ReferenciadorDeEscenas].name;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
			VelCorrer.VelocidadDeMovimiento = VelCorrer.VelocidadInicial;
		}

		if(col.gameObject.name == "SalidaSubNivelFuego" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscena = NivelDeFuego[ReferenciadorDeEscenas].name;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
			VelCorrer.VelocidadDeMovimiento = VelCorrer.VelocidadInicial;
		}

		if(col.gameObject.name == "EntradaSubNivelFuego" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas -= 1;
			NombreDeEscena = NivelDeFuego[ReferenciadorDeEscenas].name;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = false;
			VelCorrer.VelocidadDeMovimiento = VelCorrer.VelocidadInicial;
		}

		/*
		if(col.gameObject.name == "Prueba")
		{
			print("Esto anda");
		}
		*/
	}
}
