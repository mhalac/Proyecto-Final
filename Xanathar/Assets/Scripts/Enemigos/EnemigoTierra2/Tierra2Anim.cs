using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra2Anim : MonoBehaviour {

	public Tierra2 Padre;

	// Use this for initialization
	void Start ()
	{
		Padre = GetComponentInParent<Tierra2>();
	}

	public void IrALugarRandom()
	{
		Padre.IrAPosRandom();
		Padre.Animador.SetBool("Corriendo" , true);
		Padre.Animador.SetBool("Idle" , false);
	}

	public void ActivarGarraDerecha()
	{
		Padre.GarraDerecha.enabled = true;
	}

	public void ActivarGarraIzquierda()
	{
		Padre.GarraIzquierda.enabled = true;
	}

	public void ActivarElAtaque()
	{
		Padre.PermitirAtaque = true;
	}

	public void DesactivarGarraDerecha()
	{
		Padre.EstadoAtaque = 0;
		Padre.PermitirAtaque = false;

		Padre.Animador.SetBool("AtaqueDerecha" , false);
		Padre.Animador.SetBool("Corriendo" , true);

		Padre.ActivarColisiones = false;
	}

	public void DesactivarGarraIzquierda()
	{
		Padre.EstadoAtaque += 1;
		Padre.PermitirAtaque = false;

		Padre.Animador.SetBool("AtaqueIzquierda" , false);
		Padre.Animador.SetBool("Corriendo" , true);

		Padre.ActivarColisiones = false;
	}

	public void DesactivarColisionGarraDerecha()
	{
		Padre.GarraDerecha.enabled = false;
	}

	public void DesactivarColisionGarraIzquierda()
	{
		Padre.GarraIzquierda.enabled = false;
	}
}
