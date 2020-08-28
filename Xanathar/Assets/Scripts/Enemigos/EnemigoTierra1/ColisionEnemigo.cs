using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionEnemigo : MonoBehaviour {

	public Tierra1 ScriptTierra1;
	GameObject Personaje;

	EstadisticasDePersonaje EstadisticasDePersonaje;
	private bool KnockBackHecho = false;
	private bool Esperar = false;

	void Start()
	{

	}
	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Personaje")
		{
			
			if(Esperar == false)
			{
				Personaje = GameObject.FindGameObjectWithTag("Personaje");
				EstadisticasDePersonaje = FindObjectOfType<EstadisticasDePersonaje>();
				
				Esperar = true;
				ScriptTierra1.EstadoActual = ScriptTierra1.Estados[3];

				ScriptTierra1.Agente.speed = 0;
				ScriptTierra1.Animador.enabled = false;

				EstadisticasDePersonaje.RecibirDaño(ScriptTierra1.Damage);

				StartCoroutine(EsperarParaMoverserse());
				if(KnockBackHecho == false)
				{
					var dir = transform.forward;
					float Fuerza = 20;
					KnockBackHecho = true;
					StartCoroutine(KnockbackEnemigo(dir , Fuerza));
				}
			}
		}
	}

	IEnumerator KnockbackEnemigo(Vector3 Dir , float Fuerza)
	{
		for(int i = 0; i < 30; i++)
		{
			Personaje.GetComponent<CharacterController>().Move(Dir * Time.deltaTime * Fuerza);
			yield return null;
		}
		KnockBackHecho = false;
		yield return null;
	}
	
	IEnumerator EsperarParaMoverserse()
	{
		yield return new WaitForSeconds(2f);

		ScriptTierra1.Agente.speed = 9;
		ScriptTierra1.Animador.enabled = true;
		Esperar = false;
	}
}
