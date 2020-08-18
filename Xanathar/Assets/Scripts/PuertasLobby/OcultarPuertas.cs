using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OcultarPuertas : MonoBehaviour {

	public static void OcultarPuertasDelLoby()
	{
		string Lobby = SceneManager.GetActiveScene().name;

		if(Lobby == "Lobby")
		{
			if(ControlarPuertasYJefes.JefeDeFuegoMuerto == true)
			{
				GameObject PuertaFuego = GameObject.Find("EntradaNivelFuego");
				PuertaFuego.SetActive(false);
			}
		}
	}
}
