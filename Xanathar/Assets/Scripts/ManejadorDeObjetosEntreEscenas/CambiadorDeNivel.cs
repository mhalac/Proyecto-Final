using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour {

	//Scene Escenaxd;

	public Animator AnimacionDeCambioDeNivel;
	BuscadorDePos buscadorDePos;
	AsyncOperation asyncOperation;

	// Use this for initialization
	void Start ()
	{
		buscadorDePos = GameObject.FindObjectOfType<BuscadorDePos>();	
	}
	
	// Update is called once per frame
	void Update ()
	{
		/*
		Escenaxd = SceneManager.GetActiveScene();
		print(Escenaxd.name);
		*/
	}

	public void CambiarDeNivel(string NombreDeEscena)
	{
		/*
		SceneManager.LoadScene(ManejadorDeEscenas.NombreDeEscena);
		AnimacionDeCambioDeNivel.SetTrigger("Aparecer");
		ManejadorDeEscenas.ActivadorDeCambio = false;
		*/
		
		
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
		buscadorDePos.ManejadorDePos();
	}
}
