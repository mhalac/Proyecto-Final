using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour {

	//Scene Escenaxd;

	public Animator AnimacionDeCambioDeNivel;
	public BuscadorDePos BuscadorDePos;
	AsyncOperation asyncOperation;

	public void IniciadorDeCambio()
	{
		AnimacionDeCambioDeNivel.SetTrigger("Desaparecer");
		ManejadorDeEscenas.ActivadorDeCambio = true;
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
		}
	}
}
