using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("EscenasNivelDeFuego")]
	public Object[] NivelDeFuego = new Object[4];

	[Header("EscenasDelNivelEnElQueEstas")]
	public Object[] NivelCargado;

	[Header("LobbyDeNivel")]
	public Object Lobby;

	public int ReferenciadorDeEscenas = 0;
	public static bool ActivadorDeCambio = false;
	public static bool EntrasteOSaliste = false;
	public static string NombreDeEscena = "";
	CambiadorDeNivel AsignadorDeEscenas;

	// Use this for initialization
	void Start ()
	{
		AsignadorDeEscenas = FindObjectOfType<CambiadorDeNivel>();
	}
	
	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "EntradaNivelFuego" && ActivadorDeCambio == false)
		{
			NivelCargado = NivelDeFuego;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = NivelCargado[ReferenciadorDeEscenas].name;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;

			Debug.Log("Entro");
		}

		if(col.gameObject.name == "SalidaSubNivelFuego" && ActivadorDeCambio == false)
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscena = NivelCargado[ReferenciadorDeEscenas].name;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "EntradaSubNivelFuego" && ActivadorDeCambio == false)
		{
			if(ReferenciadorDeEscenas == 0)
			{
				NombreDeEscena = Lobby.name;
				AsignadorDeEscenas.IniciadorDeCambio();
				EntrasteOSaliste = false;
			}
			else
			{
				ReferenciadorDeEscenas -= 1;
				NombreDeEscena = NivelCargado[ReferenciadorDeEscenas].name;
				AsignadorDeEscenas.IniciadorDeCambio();
				EntrasteOSaliste = false;
			}
		}
	}

	public void VolverAlLobby()
	{
		ReferenciadorDeEscenas = 0;
		NombreDeEscena = Lobby.name;
		AsignadorDeEscenas.IniciadorDeCambio();
		EntrasteOSaliste = true;
	}
}
