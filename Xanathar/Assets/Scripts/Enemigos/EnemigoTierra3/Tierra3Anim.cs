using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra3Anim : MonoBehaviour {

	public Tierra3 Padre;

	public void IrALugarRandom()
	{
		Padre.IrAPosicionRandom();
		Padre.Animador.SetBool("Caminando" , true);
		Padre.Animador.SetBool("Idle" , false);
	}

	public void TerminarAtaque()
	{
		Padre.Agente.isStopped = false;

		Padre.Animador.SetBool("Atacando" , false);
		Padre.Animador.SetBool("Caminando" , true);

		Padre.PermitirColision = false;
		Padre.PermitirAtaque = false;
		Padre.PermitirRotacion = false;

		if(Vector3.Distance(Padre.Puño.transform.position , Padre.Personaje.transform.position) <= 1.4f)
		{
			Padre.EstadoActual = Padre.Estados[3];
		}
		else
		{
			Padre.EstadoActual = Padre.Estados[2];
		}
	}

	public void EncenderColision()
	{
		Padre.PuñoIzquierdo.enabled = true;
	}

	public void ApagarColision()
	{
		Padre.PuñoIzquierdo.enabled = false;
	}
}
