using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CambiadorDeNivel : MonoBehaviour {

	public Animator AnimacionDeCambioDeNivel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void CambiarDeNivel(string NombreDeNivel)
	{
		SceneManager.LoadScene(ManejadorDeEscenas.NombreDeEscena);
		AnimacionDeCambioDeNivel.SetTrigger("Aparecer");
		ManejadorDeEscenas.ActivadorDeCambio = false;
	}

	public void IniciadorDeCambio()
	{
		AnimacionDeCambioDeNivel.SetTrigger("Desaparecer");
		ManejadorDeEscenas.ActivadorDeCambio = true;
	}
}
