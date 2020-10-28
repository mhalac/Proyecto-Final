using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class ManejadorDeEscenas : MonoBehaviour {

	[Header("EscenasNivelDeFuego")]
	public string [] EscenasNivelFuego = new string[4];

	[Header("EscenasNivelDeRoca")]
	public string [] EscenasNivelRoca = new string[4];

	[Header("EscenasNivelDeViento")]
	public string [] EscenasNivelViento = new string[4];

	[Header("EscenasNivelDeAgua")]
	public string [] EscenasNivelAgua = new string[4];

	[Header("EscenasDelNivelEnElQueEstas")]
	public string[] NivelCargado = new string[4];

	[Header("LobbyDeNivel")]
	public string Lobby;

	[Header("NivelFinal")]
	public string ArenaFinal;

	public int ReferenciadorDeEscenas = 0;
	public static bool ActivadorDeCambio = false;
	public static bool EntrasteOSaliste = false;
	public static string NombreDeEscena = "";
	CambiadorDeNivel AsignadorDeEscenas;
	public GestorItems c;
	public Ataque AtaqueVariables;

	public static bool EntreNivelAgua = false;
	public static bool EntreNivelFuego = false;
	public static bool EntreNivelTierra = false;
	public static bool EntreNivelViento = false;
	public static bool EntreNivelFinal = false;

	void Awake()
	{
		AsignadorDeEscenas = FindObjectOfType<CambiadorDeNivel>();

		c = FindObjectOfType<GestorItems>();
		AtaqueVariables = FindObjectOfType<Ataque>();
	}
	
	void OnCollisionEnter(Collision col)
	{
		
		if(col.gameObject.name == "EntradaNivelFuego" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			EntreNivelFuego = true;

			NivelCargado = EscenasNivelFuego;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = EscenasNivelFuego[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "EntradaNivelRoca" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			EntreNivelTierra = true;

			NivelCargado = EscenasNivelRoca;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = EscenasNivelRoca[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "EntradaNivelViento" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			EntreNivelViento = true;

			NivelCargado = EscenasNivelViento;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = EscenasNivelViento[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "EntradaNivelAgua" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			EntreNivelAgua = true;

			NivelCargado = EscenasNivelAgua;
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = EscenasNivelAgua[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}
		
		if(col.gameObject.name == "EntradaBoss" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon") && ControlarPuertasYJefes.JefeDeAguaMuerto == true && ControlarPuertasYJefes.JefeDeFuegoMuerto == true && ControlarPuertasYJefes.JefeDeTierraMuerto == true && ControlarPuertasYJefes.JefeDeVientoMuero == true)
		{
			Debug.Log("Preparate para ser re contra re funado");

			EntreNivelFinal = true;
			NombreDeEscena = ArenaFinal;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		
		if(col.gameObject.name == "Salida" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			ReferenciadorDeEscenas += 1;
			NombreDeEscena = NivelCargado[ReferenciadorDeEscenas];
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = true;
		}

		if(col.gameObject.name == "Entrada" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
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

		if(col.gameObject.name == "PuertaLobby" && ActivadorDeCambio == false && !c.IsInvoking("DesaparecerClon"))
		{
			ReferenciadorDeEscenas = 0;
			NombreDeEscena = Lobby;
			AsignadorDeEscenas.IniciadorDeCambio();
			EntrasteOSaliste = false;
		}
		
	}
	public void VolverAlLobby()
	{
		AtaqueVariables.Reset(0);
		AtaqueVariables.Reset(1);
		AtaqueVariables.Reset(2);
		AtaqueVariables.Reset(3);

//		Debug.Log("Esto se ejecuta x veces");

		//c.CancelInvoke("DesaparecerClon");

		ReferenciadorDeEscenas = 0;
		NombreDeEscena = Lobby;
		AsignadorDeEscenas.IniciadorDeCambio();
		EntrasteOSaliste = true;
	}
}
