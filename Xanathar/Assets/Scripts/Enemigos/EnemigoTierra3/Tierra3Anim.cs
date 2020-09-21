using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tierra3Anim : MonoBehaviour {

	public Tierra3 Padre;
	public void IrALugarRandom()
	{
		Padre.IrAPosRandom();
		Padre.Animador.SetBool("Caminando" , true);
		Padre.Animador.SetBool("Idle" , false);
	}

	public void TerminarAtaque()
	{

	}

	public void EncenderColision()
	{

	}

	public void ApagarColision()
	{
		
	}
}
