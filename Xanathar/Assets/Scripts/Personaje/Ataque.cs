using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque : MonoBehaviour {

	private float CDInicial;
	private float CDTotal;
	
	// Use this for initialization
	void Start () {
		CDInicial = GetComponent<EstadisticasDePersonaje>().VelocidadDeAtaque;
		CDTotal = CDInicial;
	}
	
	// Update is called once per frame
	void Update () {
		Atacar();
	}
	void Atacar()
	{
		if(Input.GetKeyDown(KeyCode.Mouse0))
		{

		}

	}
}
