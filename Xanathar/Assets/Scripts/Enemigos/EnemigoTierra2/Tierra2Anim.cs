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
	}
}
