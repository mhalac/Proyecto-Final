using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("EscenasNivelDeFuego")]
	public string [] EscenasNivelFuego = new string[4];

	[Header("EscenasDelNivelEnElQueEstas")]
	public string[] NivelCargado = new string[4];

	[Header("LobbyDeNivel")]
	public string Lobby;

	public int ReferenciadorDeEscenas = 0;
	public static bool ActivadorDeCambio = false;
	public static bool EntrasteOSaliste = false;
	public static string NombreDeEscena = "";
	CambiadorDeNivel AsignadorDeEscenas;

	void Awake()
	{
		AsignadorDeEscenas = FindObjectOfType<CambiadorDeNivel>();
	}
	
	void OnCollisionEnter(Collision col)
	{
		
		if(col.gameObject.name == "EntradaNivelFuego" && ActivadorDeCambio == false)
		{
			NivelCargado = EscenasNivelFuego;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = EscenasNivelFuego[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}
		
		if(col.gameObject.name == "SalidaSubNivelFuego" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscena = NivelCargado[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "EntradaSubNivelFuego" && ActivadorDeCambio == false)
		{
			if(ReferenciadorDeEscenas == 0)
			{
				NombreDeEscena = Lobby;
				AsignadorDeEscenas.IniciadorDeCambio();
				EntrasteOSaliste = false;
			}
			else
			{
				ReferenciadorDeEscenas -= 1;
				NombreDeEscena = NivelCargado[ReferenciadorDeEscenas];
				AsignadorDeEscenas.IniciadorDeCambio();
				EntrasteOSaliste = false;
			}
		}

		if(col.gameObject.name == "PuertaLobby")
		{
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = Lobby;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = false;
		}
		
	}
	public void VolverAlLobby()
	{
		ReferenciadorDeEscenas = 0;
		NombreDeEscena = Lobby;
		AsignadorDeEscenas.IniciadorDeCambio();
		EntrasteOSaliste = true;
	}
}
