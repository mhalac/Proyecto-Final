using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra1 : MonoBehaviour {

	private RaycastHit hit;
	private Transform UltimaPosicion;

	[Header("Transforms Seleccionables")]

	public Transform RangoMinimo;

	[Header("Parametros")]
	public float AreaIdle;
	public int AlcanceMaximo;
	public string EstadoActual;
	private NavMeshAgent Agente;




	private string [] Estados = {"Idle" , "Chasing" , "Searching"};
	private Vector3 PosicionEnSpawn;
	private float DelayInicial;
	public float Damage;
	public Transform Heredar;
	private Vector3 Destino;
	public float Delay;
	private int EMask;
	private int PMask;
	private GameObject Personaje;


	// Use this for initialization
	void Start ()
	{
		PosicionEnSpawn = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();
		RangoMinimo.position = new Vector3(Heredar.position.x , Heredar.position.y , + AlcanceMaximo);
		EstadoActual = Estados[0];
		DelayInicial = Delay;

		PMask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected()
	{
		
	}
}
