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
	public NavMeshAgent Agente;

	public Animator Animador;

	public string [] Estados = {"Idle" , "Chasing" , "Searching" , "Stopped"};
	private Vector3 PosicionEnSpawn;
	public Transform Heredar;
	private Vector3 Destino;
	private int EMask;
	private int PMask;
	private GameObject Personaje;


	public float Damage;
	
	// Use this for initialization
	void Start ()
	{
		PosicionEnSpawn = Heredar.position;
		Agente = GetComponent<NavMeshAgent>();
		RangoMinimo.position = new Vector3(Heredar.position.x , Heredar.position.y , Heredar.position.z + AlcanceMaximo);
		EstadoActual = Estados[0];

		PMask = LayerMask.NameToLayer("Personaje");
		EMask = LayerMask.NameToLayer("Enemigo");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Agente.velocity.magnitude < 1f && EstadoActual != Estados[1])
		{
			FindObjectOfType<PositionManager>().Llegue(Destino);
			Idle();
		}
		
		if(BuscarPersonaje() && PuedoVer())
		{
			Acercar();
		}
		else if(EstadoActual == Estados[1])
		{
			Buscar();
		}
	}

	private void IrAPosRandom()
	{
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

			Destino = FindObjectOfType<PositionManager>().GenerarPosicionRandom(PosicionEnSpawn , AreaIdle , Heredar.position);
			Agente.destination = Destino;
		}
	}

	private void Idle()
	{
		Agente.isStopped = false;
		EstadoActual = Estados[0];

		IrAPosRandom();
	}

	private void Buscar()
	{
		//El enemigo se va a dirijir a la ultima posicion del jugador
		UltimaPosicion = Personaje.transform;
		Agente.isStopped = false;

		EstadoActual = Estados[2];
	}

	private bool PuedoVer()
	{
		var direccion = Personaje.transform.position - Heredar.position;

		if(Physics.Raycast(RangoMinimo.position , direccion , out hit , AlcanceMaximo , EMask) && hit.collider.gameObject.tag != "Personaje")
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	private bool BuscarPersonaje()
	{
		//Genera una esfera alrededor del personaje para ver si lo encontro, devulve true si lo encontro

		Collider [] obj = Physics.OverlapSphere(RangoMinimo.position , AlcanceMaximo);

		for(int i = 0; i < obj.Length; i++)
		{
			if(obj[i].gameObject.layer == PMask)
			{
				Personaje = obj[i].gameObject;
				return true;
			}
		}

		return false;
	}

	private bool TengoQueAcercarme()
	{
		Collider [] Obj = Physics.OverlapSphere(RangoMinimo.position , AlcanceMaximo);

		for(int i = 0; i < Obj.Length; i++)
		{	
			
			if(Obj[i].gameObject.layer == PMask)
			{
				return true;
			}
			
			
			if(Obj[i].gameObject.tag == "Enemigo")
			{
				float Distancia = Vector3.Distance(Obj[i].gameObject.transform.position , Heredar.transform.position);

				if(Distancia < 2f)
				{
					return false;
				}
			}
		}

		return false;
	}

	private void Acercar()
	{
		Agente.destination = Personaje.transform.position;
		EstadoActual = Estados[1];
	}

	private void EstaAdentro(bool TengoQueAcercarme)
	{
		if(TengoQueAcercarme)
		{
			EstadoActual = Estados[1];
			Agente.isStopped = false;
			Acercar();
		}
		else
		{
			Buscar();
		}
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.blue;
		Vector3 AreaCubo = new Vector3(AreaIdle * 2 , 2 , AreaIdle * 2);
		Gizmos.DrawWireCube(PosicionEnSpawn , AreaCubo);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(Destino , 0.5f);

		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(RangoMinimo.position , AlcanceMaximo);
	}
}
