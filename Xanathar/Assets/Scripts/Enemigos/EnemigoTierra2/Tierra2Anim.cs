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
	
	// Update is called once per frame
	void Update () {
		
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

		Padre.PermitirAtaque = true;
	}

	public void ActivarGarraIzquierda()
	{
		Padre.GarraIzquierda.enabled = true;

		Padre.PermitirAtaque = true;
	}

	public void DesactivarGarraDerecha()
	{
		Padre.GarraDerecha.enabled = false;

		Padre.EstadoAtaque = 0;
		Padre.PermitirAtaque = false;

		Padre.Animador.SetBool("AtaqueDerecha" , false);
		Padre.Animador.SetBool("Corriendo" , true);
	}

	public void DesactivarGarraIzquierda()
	{
		Padre.GarraIzquierda.enabled = false;

		Padre.EstadoAtaque += 1;
		Padre.PermitirAtaque = false;

		Padre.Animador.SetBool("AtaqueIzquierda" , false);
		Padre.Animador.SetBool("Corriendo" , true);
	}
}
