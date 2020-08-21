﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tierra2 : MonoBehaviour {

	private RaycastHit Hit;
	private Transform UltimaPosicion;

	[Header("Transforms Seleccionables")]
	public Transform Heredar;
	public Transform AreaAtaque;

	[Header("Box Colliders Garras")]
	public BoxCollider GarraIzquierda;
	public BoxCollider GarraDerecha;

	[Header("Parametro")]
	public float RangoIdle;
	public int RangoMaximo;
	public float rangoAtaque;
	public NavMeshAgent Agente;
	public Animator Animador;
	public Vector3 PosicionEnSpawn;
	public float DelayEntreAtaques;

	public string [] Estados = {"Idle" , "Searching" , "Chasing" , "Attack"};
	public string EstadoActual;
	private Vector3 Destino;
	private GameObject Personaje;

	private int PMask;
	private int EMask;

	public int EstadoAtaque;

	// Use this for initialization
	void Start ()
	{
		PosicionEnSpawn = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();
		EstadoActual = Estados[0];

		PMask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");

		GarraIzquierda.enabled = false;
		GarraDerecha.enabled = false;

		Animador.SetBool("Corriendo" , false);
		EstadoAtaque = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if(Agente.remainingDistance < Mathf.Epsilon && EstadoActual == Estados[1])
		{
			Debug.Log("Llegue");
			EstadoActual = Estados[0];
			Animador.SetBool("Corriendo" , false);
		}
		
		if(BuscarPersonaje() && PuedoVer() && EstadoActual!= Estados[2])
		{
			EstadoActual = Estados[2];
		}

		if(EstadoActual == Estados[2])
		{
			Acercar();
			Animador.SetBool("Corriendo" , true);
		}
	}

	public void IrAPosRandom()
	{
		EstadoActual = Estados[1];
		if(Agente.remainingDistance > Mathf.Epsilon)
		{
			Agente.destination = Destino;
		}
		else
		{
			if(FindObjectOfType<PositionManager>().EstaOcupado(Destino))
			{
				FindObjectOfType<PositionManager>().Llegue(Destino);
			}

			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionEnSpawn , RangoIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	public bool PuedoVer()
	{
		var Direccion = Personaje.transform.position - Heredar.position;

		if(Physics.Raycast(Heredar.position , Direccion , out Hit , RangoMaximo , EMask) && Hit.collider.gameObject.tag != "Personaje")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	public bool BuscarPersonaje()
	{
		Collider [] Obj = Physics.OverlapSphere(Heredar.position , RangoMaximo);

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.layer == PMask)
			{
				Personaje = Obj[i].gameObject;
				return true;
			}
		}

		return false;
	}

	public void Acercar()
	{
		Agente.destination = Personaje.transform.position;
		EstadoActual = Estados[2];
		StartCoroutine(GirarAlPersonaje());
	}

	public void Atacar()
	{
		/*
		Collider [] Obj = Physics.OverlapBox(Heredar.position , new Vector3(rangoAtaque , rangoAtaque , rangoAtaque));

		for(int i = 0; i < Obj.Length; i++)
		{
			if(Obj[i].gameObject.layer == PMask)
			{
				EstadoActual = Estados[3];
				Debug.Log("Atacar");
			}
		}
		*/
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(Heredar.position , RangoMaximo);

		Gizmos.color = Color.green;
		Vector3 AreaCuboAtaque = new Vector3(rangoAtaque , rangoAtaque , rangoAtaque);
		Gizmos.DrawWireCube(AreaAtaque.position , AreaCuboAtaque);

		Gizmos.color = Color.red;
		Vector3 AreaIdle = new Vector3(RangoIdle * 2 , 2 , RangoIdle * 2);
		Gizmos.DrawWireCube(PosicionEnSpawn , AreaIdle);

		Gizmos.color = Color.black;
		Gizmos.DrawSphere(Destino ,0.5f);
	}



	IEnumerator GirarAlPersonaje()
	{
		Vector3 Dir = Personaje.transform.position - transform.position;
		Dir.y = 0;
		Quaternion Rot = Quaternion.LookRotation(Dir);

		transform.rotation = Quaternion.Lerp(transform.rotation , Rot , 4 * Time.deltaTime);
		yield return new WaitForEndOfFrame();
	}
}
