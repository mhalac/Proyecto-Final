using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour {

	public bool PermitirCambios = false;
	public Animator AnimacionDeCambioDeNivel;
	public BuscadorDePos BuscadorDePos;
	AsyncOperation asyncOperation;
	public void IniciadorDeCambio()
	{
		if(PermitirCambios == false)
		{
			PermitirCambios = true;
			AnimacionDeCambioDeNivel.SetTrigger("Desaparecer");
			ManejadorDeEscenas.ActivadorDeCambio = true;
		}
	}

	IEnumerator CargarEscena()
	{
		asyncOperation = SceneManager.LoadSceneAsync (ManejadorDeEscenas.NombreDeEscena , LoadSceneMode.Additive);

		asyncOperation.allowSceneActivation = true;
		yield return asyncOperation;
		StartCoroutine(BorrarEscena());
	}

	IEnumerator BorrarEscena()
	{
		string EscenaActual = SceneManager.GetActiveScene().name;
		asyncOperation = SceneManager.UnloadSceneAsync(EscenaActual);

		asyncOperation.allowSceneActivation = true;
		while(!asyncOperation.isDone)
		{
			AnimacionDeCambioDeNivel.SetTrigger("Aparecer");
			ManejadorDeEscenas.ActivadorDeCambio = false;
			yield return asyncOperation;


			BuscadorDePos.ManejadorDePos();
			PermitirCambios = false;
			MuerteEnVacios.Caiste = false;
			
			if(SceneManager.GetActiveScene().name == "Lobby")
			{
				if(ControlarPuertasYJefes.JefeEliminado == true)
				{
					EstadisticasDePersonaje estadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();

					if(estadisticasDePersonaje.MurioDespuesDeMatarJefe == true)
					{
						AgarradorDeItems agarradorDeItems = FindObjectOfType<AgarradorDeItems>();
						DropearItemsEnSpawn.TirarItemsEnSpwn(agarradorDeItems.CopiaDeObjetos);
						ControlarPuertasYJefes.JefeEliminado = false;
						estadisticasDePersonaje.MurioDespuesDeMatarJefe = false;
						
					}
					else
					{
						ControlarPuertasYJefes.JefeEliminado = false;
					}
				}
				

				if(ControlarPuertasYJefes.JefeDeFuegoMuerto == true)
				{
					GameObject PuertaDeFuego = GameObject.Find("EntradaNivelFuego");
					PuertaDeFuego.SetActive(false);
				}

				if(ControlarPuertasYJefes.JefeDeTierraMuerto == true)
				{
					GameObject PuertaDeTierra = GameObject.Find("EntradaNivelRoca");
					PuertaDeTierra.SetActive(false);
				}

				if(ControlarPuertasYJefes.JefeDeVientoMuero == true)
				{
					GameObject PuertaDeViento = GameObject.Find("EntradaNivelViento");
					PuertaDeViento.SetActive(false);
				}
			}
		}
	}

	public void BuscadorDeFuncionParaGuardarItems()
	{
		ListasDeItems FuncionParaGuardar = FindObjectOfType<ListasDeItems>();
		FuncionParaGuardar.LlenarElConstructor();
	}

	public void BuscadorFuncionParaCargarItems()
	{
		ListasDeItems FuncionParaCargar = FindObjectOfType<ListasDeItems>();
		FuncionParaCargar.InstanciarLosObjetosDelConstructor();
	}
}
