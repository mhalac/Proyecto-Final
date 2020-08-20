using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra2 : MonoBehaviour {

	private RaycastHit Hit;
	private Transform UltimaPosicion;

	[Header("Transforms Seleccionables")]
	public Transform RangoMinimo;
	public Transform Heredar;

	[Header("Parametro")]
	public float AreaIdle;
	public int AlcanceMaximo;
	public NavMeshAgent Agente;
	public Animator Animador;
	public Vector3 PosicionEnSpawn;
	public float DelayEntreAtaques;

	string [] Estados = {"Idle" , "Searching" , "Attack"};
	public string EstadoActual;


	private GameObject Personaje;
	public float Damage;

	private int PMask;
	private int EMask;

	// Use this for initialization
	void Start ()
	{
		PosicionEnSpawn = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();
		RangoMinimo.position = new Vector3(RangoMinimo.position.x , RangoMinimo.position.y , RangoMinimo.position.z + AlcanceMaximo);
		EstadoActual = Estados[0];

		PMask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(RangoMinimo.position , AlcanceMaximo);
	}
}
