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
	
	// Update is called once per frame
	void Update () {
		
	}
}
