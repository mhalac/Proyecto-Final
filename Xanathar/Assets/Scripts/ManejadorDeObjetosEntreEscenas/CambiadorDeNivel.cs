using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour {

	//Scene Escenaxd;

	public Animator AnimacionDeCambioDeNivel;
	public BuscadorDePos BuscadorDePos;
	AsyncOperation asyncOperation;

	// Use this for initialization
	void Start ()
	{
	}

	public void CambiarDeNivel(string NombreDeEscena)
	{
		StartCoroutine(CargarEscena());
		AnimacionDeCambioDeNivel.SetTrigger("Aparecer");
		ManejadorDeEscenas.ActivadorDeCambio = false;
		
	}

	public void IniciadorDeCambio()
	{
		AnimacionDeCambioDeNivel.SetTrigger("Desaparecer");
		ManejadorDeEscenas.ActivadorDeCambio = true;
	}

	IEnumerator CargarEscena()
	{
		string EscenaActual = SceneManager.GetActiveScene().name;
		asyncOperation = SceneManager.LoadSceneAsync (ManejadorDeEscenas.NombreDeEscena , LoadSceneMode.Additive);

		asyncOperation.allowSceneActivation = true;
		yield return asyncOperation;
		SceneManager.UnloadSceneAsync(EscenaActual);
	}

	public void LlamadorDeFuncion()
	{
		BuscadorDePos.ManejadorDePos();
	}
}
