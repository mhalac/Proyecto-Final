using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra1 : MonoBehaviour {

	private RaycastHit hit;
	private Transform UltimaPosicion;

	[Header("Transforms Seleccionables")]
	public Transform VisionDeDisparo;
	public Transform RangoMinimo;

	[Header("Parametros")]
	public float AreaIdle;
	public string NombreHijo;
	public int AlcanceMaximo;
	public string EstadoActual;
	private NavMeshAgent Agente;




	private string [] Estados = {"Idle" , "Chasing" , "Searching"};
	private Vector3 PosicionEnSpawn;
	private float DelayInicial;
	public float Damage;
	private Transform Heredar;
	private Vector3 Destino;
	public float Delay;
	private int EMask;
	private int PMask;
	private GameObject Personaje;


	// Use this for initialization
	void Start ()
	{
		Heredar = transform.Find(NombreHijo).GetComponent<Transform>();
		Debug.Log(Heredar.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDrawGizmosSelected()
	{
		
	}
}
