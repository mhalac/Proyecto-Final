using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegeneracionVida : MonoBehaviour {

	EstadisticasDePersonaje Estadisticas;
	ManejadorDeItems ManejadorDeItems;

	// Use this for initialization
	void Start ()
	{
		Estadisticas = FindObjectOfType<EstadisticasDePersonaje>();
		ManejadorDeItems = FindObjectOfType<ManejadorDeItems>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.L))
		{
			StartCoroutine(RegenerarVida());
		}	
	}

	public IEnumerator RegenerarVida()
	{
		/*
		float CoolDownCurarse = Estadisticas.TiempoDeRegeneracion;

		while(ManejadorDeItems.CorrutinaFuncionando == true)
		{
			while(ManejadorDeItems.ActivadorRegeneracionDeVida == true)
			{
				CoolDownCurarse -= Time.deltaTime;
				yield return new WaitForEndOfFrame();
				Debug.Log(CoolDownCurarse);

				if(CoolDownCurarse <= 0f)
				{
					Estadisticas.VidaActualPersonaje += 1;
					ManejadorDeItems.CorrutinaFuncionando = false;
					ManejadorDeItems.ManejadorDeVida();
					yield break;
				}
			}
			yield break;
		}
		*/

		float CoolDownCurarse = Estadisticas.TiempoDeRegeneracion;
		if(ManejadorDeItems.CorrutinaFuncionando == true)
		{
			yield break;
		}
		
		ManejadorDeItems.CorrutinaFuncionando = true;

		while(ManejadorDeItems.ActivadorRegeneracionDeVida == true)
		{
			CoolDownCurarse -= Time.deltaTime;
			yield return new WaitForEndOfFrame();
			Debug.Log(CoolDownCurarse);

			if(CoolDownCurarse <= 0f)
			{
				Estadisticas.VidaActualPersonaje += 1;
				ManejadorDeItems.CorrutinaFuncionando = false;
				ManejadorDeItems.ManejadorDeVida();
				yield break;
			}
		}
	}
}
